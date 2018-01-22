using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolrdState
{
    DayTime = 0, // 낮
    DuskTime, // 밤
}
public class WorldTimer : MonoBehaviour
{
    // 사운드
    private AudioSource AS_Audio;
    public AudioClip[] AC_Sound;

    public GameObject _LightWolrd; // 낮,밤 구분하기위해 라이트끄기
    public static WorldTimer _instance = null; // 싱글턴

    private MobCreate _MobCreate = null; // 몬스터 생성부분 스크립트 접근하기
    public WolrdState _WorldState = WolrdState.DayTime; // 현재 낮,밤 인지 구별

    public int m_nMonth = 1; // 1일부터
    public int m_nDayTime = 0; // 시간시작해서 
    public int m_nDaxDayTime = 60; // 몆초까지 유지할건지 (낮 <-> 밤) 바뀌는 딜레이


    // 몬스터 부분
    public int m_nCreateMobTime = 3; // 몬스터 생성시간 몆초씩
    public Vector3 m_vMobPos; // 몬스터를 어디서부터 생성 시킬 것 인지
    // 몬스터 생성 몆마리 할 것인지
    public int m_nCreateMinMob = 0; 
    public int m_nCreateMaxMob = 5;


    void Start ()
    {
        // 사운드
        AS_Audio = this.gameObject.AddComponent<AudioSource>();
        AS_Audio.clip = this.AC_Sound[0];
        AS_Audio.loop = false;

        WorldTimer._instance = this;
        _MobCreate = GetComponent<MobCreate>();
        StartCoroutine(DayTimer());
	}
	
	void Update ()
    {
        // 시간이 60초 지났을경우





        if(m_nDayTime >= m_nDaxDayTime)
        {
            // 현재 상태가 낮이면 밤으로 바꿔주고
            if (_WorldState == WolrdState.DayTime)
            {
                SoundPlayType(1,true);
                _LightWolrd.SetActive(false);
                m_nCreateMinMob = 0;
                StartCoroutine("MobCreateTime");
                _WorldState = WolrdState.DuskTime;
            }
            else
            {
                // 밤이면 일수 올려준 후 낮으로 바꿈
                PlayerStatReset();
                _WorldState = WolrdState.DayTime;
            }
            m_nDayTime = 0;
        }

        // 현재 7일날 일경우
        if(m_nDayTime == 7)
        {
            // 그에따른 행동
        }
        StateDay();
    }
    void PlayerStatReset()
    {
        SoundPlayType(0,true);
        m_nMonth++;
        _LightWolrd.SetActive(true);
        PlayerMove.m_fPlayerEnergy = 10f;
        PlayerMove.m_fPlayerHp = 10f;
    }
    void StateDay()
    {
        // 낮과 밤을 알려주기 위해 사용함
        switch (_WorldState)
        {
            case WolrdState.DayTime:
                {
                    if (m_nDayTime >= 50 && m_nDayTime <= 51)
                        SoundPlayType(2,false);
                }
                break;

            case WolrdState.DuskTime:
                {
                    if (m_nDayTime >= 50 && m_nDayTime <= 51)
                        SoundPlayType(3,false);
                }
                break;
        }
    }

    void SoundPlayType(int type, bool loop)
    {
        AS_Audio.clip = this.AC_Sound[type];
        AS_Audio.Play();
        AS_Audio.loop = loop;
    }


    // 몬스터 생성
    IEnumerator MobCreateTime()
    {
        while (m_nCreateMinMob < m_nCreateMaxMob)
        {
            m_vMobPos = new Vector3(27, 1.5f, Random.Range(-5, 10));
            _MobCreate.MobCreates(m_vMobPos);
            m_nCreateMinMob++;
            yield return new WaitForSeconds(3f);
        }
    }


    // 시간을 계속 흐르기위해 코루틴으로 1초씩 올려줌
    IEnumerator DayTimer()
    {
        while(true)
        {
            m_nDayTime++;
            yield return new WaitForSeconds(1f);
        }
    }
}
