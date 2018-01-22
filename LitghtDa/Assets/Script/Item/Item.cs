using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템 획득 범위
    public float m_fItemDistance = 1.0f;

    void Update()
    {
        ItemDistance();
    }

    void ItemDistance()
    {
        // 플레이어의 좌표를 가져와서 보간하기
        Vector3 vPlayerPos = GameObject.FindWithTag("Player").transform.position;

        // 플레이어 좌표와 아이템의 좌표를 비교하여 일정거리안에 들었을시
        if(Vector3.Distance(vPlayerPos, transform.position) < m_fItemDistance)
        {
            // E키를 누르면 아이템 획득
            if (Input.GetKey(KeyCode.E))
            {
                Inventory._instance.m_bItemSelect = true;
                Destroy(this.gameObject);
            }
        }
    }
}
