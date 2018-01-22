using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlashLight : MonoBehaviour
{
    // 사운드
    private AudioSource AS_Audio;
    public AudioClip[] AC_Sound;


    // 라이트
    public GameObject _LightObj;

    private Light _Light; // 라이트의 속성값을 가져옴
    private RaycastHit hitObj; // 레이캐스트 hit정보
    [SerializeField]
    private float m_fDis = 20f; // 레이캐스트 거리 
    private float m_nDamage = 1f; // 데미지수치
    public static float m_fLightBar = 1f; // UI게이지수치

    private bool m_bLightWith = true;
    private bool m_bMobLightSound = true;
    private GameObject Mob;


    void Start ()
    {
        // 사운드
        AS_Audio = this.gameObject.AddComponent<AudioSource>();
        AS_Audio.clip = this.AC_Sound[0];
        AS_Audio.loop = false;

        // 라이트 정보 받기
        _Light = GetComponent<Light>();
    }
	
	void Update ()
    {
        LightGageAdd();

        // 손전등 on off
        if (Input.GetKeyDown(KeyCode.Q) && m_fLightBar >= 0)
        {
            if (_Light.enabled == true)
            {
                SoundPlayType(1);
                _LightObj.SetActive(false);
                _Light.enabled = false;
            }
            else
            {
                SoundPlayType(0);
                _LightObj.SetActive(true);
                _Light.enabled = true;
            }
        }

        // 손전등이 on 일경우 게이지 깍기, 레이저 쏘기 등등
        if(_Light.enabled)
        {
            // 게이지 줄여주고
            m_fLightBar -= 0.05f * Time.deltaTime;
            // 일정 게이지가 됐을경우 깜박깜박 거리게 하기
            if (m_fLightBar < 0.3f && m_bLightWith)
            {
                StartCoroutine("LightWith");
                m_bLightWith = false;
            }
            // 게이지가 다닳았을 경우 라이트OFF
            else if (m_fLightBar <= 0)
            {
                _Light.enabled = false;
                _LightObj.SetActive(false);
                m_fLightBar = 0;
            }
        }

        // 빛을 켜놨고, 게이지가 있을경우 레이캐스트 발사
        if (_Light.enabled && m_fLightBar >= 0)
        {
            RayLight();
        }
        if (m_fLightBar >= 1)
            m_fLightBar = 1;
    }
    void SoundPlayType(int type)
    {
        AS_Audio.clip = this.AC_Sound[type];
        AS_Audio.Play();
    }
    // 손전등 깜박깜박
    IEnumerator LightWith()
    {
        while (true)
        {
            for(int i = 0; i < 7; i++)
            {
                _LightObj.SetActive(false);
                _Light.enabled = false;
                yield return new WaitForSeconds(0.05f);
                _LightObj.SetActive(true);
                _Light.enabled = true;
                yield return new WaitForSeconds(0.05f);
            }
            yield break;
        }
    }
    void RayLight()
    {
        // 라이트에 레이캐스트를 통해 적 피아식별
        if (Physics.Raycast(transform.position, transform.forward, out hitObj, m_fDis))
        {
            // ?N 몹이 맞을경우
            if (hitObj.collider.gameObject.tag == "CubeMob")
            {
                // 체력깍아주기
                hitObj.collider.gameObject.GetComponent<CubeMob>().m_fHp -= 5f * Time.deltaTime;
                hitObj.collider.gameObject.GetComponent<CubeMob>().EffectMob(true);
                if (m_bMobLightSound)
                {
                    hitObj.collider.gameObject.GetComponent<MobMove>().SoundPlayType(1);
                    StartCoroutine("SoundResetTimer");
                    m_bMobLightSound = false;
                }
                // 몬스터가 빛에 맞았다는걸 알려주고 피하기
                hitObj.collider.gameObject.GetComponent<MobMove>().m_bLight = true;
            }
        }
    }
    IEnumerator SoundResetTimer()
    {
        yield return new WaitForSeconds(1f);
        m_bMobLightSound = true;
    }
    
    // 기름 손전등에 넣기
    void LightGageAdd()
    {
        if(Input.GetKeyDown(KeyCode.G)) // G키 누르면
        {
            // 갯수가 1개이상일 경우
            if(Inventory._instance.m_nItemNumber >= 1)
            {
                SoundPlayType(2); // 사운드호출
                m_bLightWith = true; 
                m_fLightBar += 0.3f; // 기름 주입
                Inventory._instance.m_nItemNumber--; // 갯수 줄이기
            }
        }
    }
}
