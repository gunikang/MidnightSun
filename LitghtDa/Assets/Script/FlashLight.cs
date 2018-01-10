using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light _Light;
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

    }
}
