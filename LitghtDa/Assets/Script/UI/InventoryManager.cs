using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public GameObject _ItemInventory;
    private bool m_bCheaktroy = false;

	void Update ()
    {
        _ItemInventory.SetActive(m_bCheaktroy);
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (m_bCheaktroy)
                m_bCheaktroy = false;
            else
                m_bCheaktroy = true;
        }
    }
}
