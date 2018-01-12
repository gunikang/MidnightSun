using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light _Light;
    RaycastHit hitObj;
    [SerializeField]
    private float m_fDis = 20f;
    private float m_nDamage = 1f;
    void Start ()
    {
        _Light = GetComponent<Light>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_Light.enabled == true)
                _Light.enabled = false;
            else
                _Light.enabled = true;
        }

        if(_Light.enabled)
        {
            UIManager._instance._FlashLight.fillAmount -= 0.05f * Time.deltaTime;
            if (UIManager._instance._FlashLight.fillAmount <= 0)
            {
                _Light.enabled = false;
                UIManager._instance._FlashLight.fillAmount = 0;
            }
        }
        if (_Light.enabled && UIManager._instance._FlashLight.fillAmount > 0)
            RayLight();
        
    }

    void RayLight()
    {
        
        // 라이트에 레이캐스트를 통해 적 피아식별
        if (Physics.Raycast(transform.position, transform.forward, out hitObj, m_fDis))
        {
            if (hitObj.collider.gameObject.tag == "CubeMob")
            {
                hitObj.collider.gameObject.GetComponent<CubeMob>().m_fHp -= 5f * Time.deltaTime;
                Debug.Log(hitObj.collider.gameObject.GetComponent<CubeMob>().m_fHp);
            }
        }
    }
}
