using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // 사운드
    private AudioSource AS_Audio;
    public AudioClip[] AC_Sound;

    public static float m_fPlayerHp = 10; // 캐릭터의 Hp
    public static float m_fPlayerEnergy = 1; // 캐릭터의 행동에너지
    public bool m_bEnergyCheak = true; // 행동에너지(사운드처리)

    public static float speed;  // 캐릭터 움직임 스피드.      
    public float jumpSpeed;     // 캐릭터 점프 힘.
    public float gravity;       // 캐릭터에게 작용하는 중력.

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.

    void Awake()
    {
        
        // 사운드
        AS_Audio = this.gameObject.AddComponent<AudioSource>();
        AS_Audio.clip = this.AC_Sound[0];
        AS_Audio.loop = false;

        speed = 3.0f;
        jumpSpeed = 5.0f;
        gravity = 10.0f;

        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        StartCoroutine(StaminaAdd());
    }
    void Update()
    {
        UIManager._instance._Stamina.fillAmount = m_fPlayerEnergy;
        if (m_fPlayerEnergy >= 1) m_fPlayerEnergy = 1;

        Debug.Log("m_fPlayerHp : " + m_fPlayerHp);
        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded)
        {
            // 위, 아래 움직임 셋팅. 
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
            MoveDir = transform.TransformDirection(MoveDir);

            // 스피드 증가.
            MoveDir *= speed;

            // 캐릭터 점프
            if (Input.GetButton("Jump"))
                MoveDir.y = jumpSpeed;
        }

        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;
        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);

        PlayerStet();

    }

    void PlayerStet()
    {
        // 캐릭터가 죽었을 경우
        if (m_fPlayerHp <= 0)
        {

        }
        // 행동에너지가 없을 경우
        if (m_fPlayerEnergy <= 0.2f)
        {
            if (m_bEnergyCheak)
            {
                SoundOneShotPlayType(3);
                m_bEnergyCheak = false;
            }
        }
        else if (m_fPlayerEnergy <= 0)
        {
            m_fPlayerEnergy = 0;
        }
        else
            m_bEnergyCheak = true;
    }

    public void SoundPlayType(int type)
    {
        AS_Audio.clip = this.AC_Sound[type];
        AS_Audio.Play();
    }
    public void SoundOneShotPlayType(int type)
    {
        AS_Audio.PlayOneShot(AC_Sound[type]);
    }
    public void SoundStopPlayType(int type)
    {
        AS_Audio.clip = this.AC_Sound[type];
        AS_Audio.Stop();
    }

    IEnumerator StaminaAdd()
    {
        while(true)
        {
            m_fPlayerEnergy += 0.2f * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
