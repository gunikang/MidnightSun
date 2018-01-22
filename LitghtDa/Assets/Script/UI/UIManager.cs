using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance = null;
    public Image _FlashLight = null;

	void Start ()
    {
        UIManager._instance = this;
    }
}
