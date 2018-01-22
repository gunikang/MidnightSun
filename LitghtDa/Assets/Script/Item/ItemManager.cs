using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 아이템 생성 좌표
    public Transform[] _ItemPosition = null;

    // 아이템 오브젝트
    public GameObject[] _ItemObj = null;

    // 아이템을 리스트에 보관
    private List<GameObject> _ItemList = new List<GameObject>();
    
    void Start ()
    {
        for (int i = 0; i < 5; i++)
        {
            // 아이템의 생성좌표는 10개로 지정해서 랜덤돌림
            int nRand = Random.Range(0, 10); 
            // 오브젝트 생성후 좌표 넣어주기
            GameObject _obj = Instantiate(_ItemObj[0]); 
            _ItemList.Add(_obj);
            _obj.transform.position = new Vector3(_ItemPosition[nRand].position.x, _ItemPosition[nRand].position.y, _ItemPosition[nRand].position.z);
        }
    }

}
