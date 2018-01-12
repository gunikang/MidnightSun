using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMob : MonoBehaviour
{
    public float m_fHp = 10f;
    public float m_fSpeed = 3f;

    void Update ()
    {
        if (m_fHp <= 0)
        {
            m_fHp = 0;
            Destroy(gameObject);
        }

    }
}
