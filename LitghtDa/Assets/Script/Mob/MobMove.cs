using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MobState
{
    Idle = 0,
    Walk,
    LightRun,
    Attack,
}

public class MobMove : MonoBehaviour
{
    // 사운드
    private AudioSource AS_Audio;
    public AudioClip[] AC_Sound;

    public MobState _State = MobState.Idle;
    public bool m_bLight = false;
    NavMeshAgent nav;
    Vector3 vPlayerPos;
    Vector3 vRand;

    [SerializeField]
    private RaycastHit hitObj; // 레이캐스트 hit정보
    private float m_fDis = 10f; // 레이캐스트 거리 

    void Awake()
    {
        // 사운드
        AS_Audio = this.gameObject.AddComponent<AudioSource>();
        AS_Audio.clip = this.AC_Sound[0];
        AS_Audio.PlayOneShot(AC_Sound[0]);

        nav = GetComponent<NavMeshAgent>();
        nav.speed = 3f;
    }
    void Update ()
    {
        vPlayerPos = GameObject.FindWithTag("Player").transform.position;
        
        if (m_bLight)
        {
            vRand = new Vector3(Random.Range(-27, 27), 3f, Random.Range(-5, 10));
            _State = MobState.LightRun;
            m_bLight = false;
        }
        MobStateFild();
    }

    public void SoundPlayType(int type)
    {
        AS_Audio.clip = this.AC_Sound[type];
        AudioSource.PlayClipAtPoint(AC_Sound[type], transform.position);
    }

    void MobStateFild()
    {
        switch (_State)
        {
            case MobState.Idle:
                {
                    nav.speed = 3f;
                    vRand = new Vector3(Random.Range(-27, 27), 3f, Random.Range(-5, 10));
                    _State = MobState.Walk;
                }
                break;
            case MobState.Walk:
                {
                    if (Vector3.Distance(transform.position, vPlayerPos) <= 6f)
                    {
                        nav.SetDestination(vPlayerPos);
                        if (Vector3.Distance(transform.position, vPlayerPos) <= 4f)
                        {
                            _State = MobState.Attack;
                        }
                    }
                    else
                    {
                        nav.SetDestination(vRand);
                        if (Vector3.Distance(transform.position, vRand) <= 2f)
                        {
                            _State = MobState.Idle;
                        }
                    }
                }
                break;
            case MobState.LightRun:
                {
                    nav.SetDestination(vRand);
                    if (Vector3.Distance(transform.position, vRand) <= 2f)
                    {
                        _State = MobState.Idle;
                    }
                }
                break;
            case MobState.Attack:
                {
                    if (Vector3.Distance(transform.position,vPlayerPos) <= 1.5f)
                    {
                        PlayerMove.m_fPlayerHp -= 2 * Time.deltaTime;
                        _State = MobState.Idle;
                    }
                    else
                    {
                        nav.speed = 100;
                        StartCoroutine("IE_Walk");
                    }
                }

                break;
        }
    }
    IEnumerator LightRunTime()
    {
        yield return new WaitForSeconds(1f);
        m_bLight = false;
        yield return new WaitForSeconds(1f);
    }
    IEnumerator IE_Walk()
    {
        yield return new WaitForSeconds(1f);
        nav.speed = 3f;
        yield return new WaitForSeconds(0.5f);
        _State = MobState.Walk;

    }
}
