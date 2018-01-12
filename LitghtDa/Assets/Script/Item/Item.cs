using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private List<GameObject> _ItemList = new List<GameObject>();


    public GameObject _ItemText = null;
    public Transform _PlayerTf = null;

    public float m_fItemDistance = 1.5f; // 아이템 획득 범위
	void Start ()
    {
        _ItemText.SetActive(false);
    }
	
	void Update ()
    {
        ItemDistance();
    }

    void ItemDistance()
    {
        if(Vector3.Distance(_PlayerTf.transform.position, transform.position) < m_fItemDistance)
        {
            _ItemText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                _ItemText.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else
        {
            _ItemText.SetActive(false);
        }
    }

    
}
