using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Transform camTransform;
    public float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
            camTransform = GetComponent(typeof(Transform)) as Transform;
    }
    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update ()
    {
	    if(shake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0;
            camTransform.localPosition = originalPos;
        }
	}
}
