using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDLight : MonoBehaviour
{
    // Light 이펙트 오브젝트
    public GameObject _LEDLightObj;
    // Light 정보 가져오기
    public Light _LEDLight;
    // 내구도 는 5
    public int m_nDurability = 5;
    // 전등과 플레이어의 거리 수치
    public float m_fItemDistance = 3f;
    // 전등의 게이지
    public float m_fGage = 40f;

    void Update ()
    {
        // 플레이어의 좌표를 가져와서 보간하기
        Vector3 vPlayerPos = GameObject.FindWithTag("Player").transform.position;
        // 플레이어와 전등의 거리비교 
        if(Vector3.Distance(vPlayerPos, transform.position) < m_fItemDistance)
        {
            // 내구도가 있을경우
            if (m_nDurability > 0)
            {
                // "E"키를 눌렀고, 전등 게이지가 있을경우 on,off 
                if (Input.GetKeyDown(KeyCode.E) && m_fGage > 0)
                {
                    if (_LEDLight.enabled == true)
                    {
                        _LEDLightObj.SetActive(false);
                        _LEDLight.enabled = false;
                    }
                    else
                    {
                        // 내구도 하나차감
                        m_nDurability--;
                        _LEDLightObj.SetActive(true);
                        _LEDLight.enabled = true;
                    }
                }
            }
            else
            {
                // 내구도가 없을시 빛을 꺼준다.
                _LEDLight.enabled = false;
                _LEDLightObj.SetActive(false);
                m_nDurability = 0;
            }
        }
        // 전등이 켜진상태면
        if(_LEDLight.enabled)
        {
            // 게이지 감소
            m_fGage -= Time.deltaTime;
            // 게이지가 다닳면 빛꺼줌
            if(m_fGage <= 0 )
            {
                _LEDLight.enabled = false;
                _LEDLightObj.SetActive(false);
                m_fGage = 0;
            }
        }
    }
}
