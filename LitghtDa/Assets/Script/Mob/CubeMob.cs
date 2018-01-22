using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMob : MonoBehaviour
{
    public GameObject _MobEffect;
    private MobMove _MobMove = null;

    public float m_fHp = 10f;
    public float m_fScal;
    private bool[] m_bDieSound = new bool[2];

    void Start()
    {
        m_bDieSound[0] = true;
        m_bDieSound[1] = true;
        _MobMove = GetComponent<MobMove>();    
    }
    void Update ()
    {
        // 몬스터의 Scale 값을 가져오기
        transform.localScale = new Vector3(m_fScal, m_fScal, m_fScal);
        
        // 아침이 밝아지면 몬스터들 죽이기
        if(WorldTimer._instance._WorldState == WolrdState.DayTime)
        {
            if(m_bDieSound[0])
            {
                _MobMove.SoundPlayType(2);
                m_bDieSound[0] = false;
            }
            m_fHp = 0;
        }

        // 몬스터의 체력이 0일경우
        if (m_fHp <= 0)
        {
            if (m_bDieSound[1])
            {
                _MobMove.SoundPlayType(2);
                m_bDieSound[1] = false;
            }
            // Scale 값을 조정해서 죽었다고 알려주기(애니메이션 역할)
            m_fScal -= Time.deltaTime;
            if (m_fScal <= 0)
            {
                // 오브젝트 파괴
                Destroy(gameObject);
            }
        }
    }

    public void EffectMob(bool type)
    {
        _MobEffect.SetActive(type);
        StartCoroutine("EffectDie");
    }

    IEnumerator EffectDie()
    {
        yield return new WaitForSeconds(2f);
        _MobEffect.SetActive(false);
    }
}
