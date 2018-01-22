using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCreate : MonoBehaviour
{
    public GameObject _MobCreate = null;
    public void MobCreates(Vector3 vCreatePos)
    {
        GameObject _CreateObj = Instantiate(_MobCreate);
        transform.position = new Vector3(vCreatePos.x, vCreatePos.y, vCreatePos.z);
    }
}
