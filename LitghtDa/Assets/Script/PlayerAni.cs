using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    // 캐릭터의 애니메이터 속성값 가져오기
    private Animator _PlayerAni = null;
    private PlayerMove _PlayerMove = null;
    
	void Start ()
    {
        // 애니메이터 값
        _PlayerAni = GetComponent<Animator>();

        _PlayerMove = GetComponent<PlayerMove>();

    }
	
	void Update ()
    {
        // "W" or "S" 키를 눌렀을경우
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Shift(달리기) 키를 눌렀을경우 // 플레이어의 행동에너지 0이상일 경우 
            if(Input.GetKey(KeyCode.LeftShift) && PlayerMove.m_fPlayerEnergy >= 0)
            {
                // 행동에너지 감소
                PlayerMove.m_fPlayerEnergy -= 2 * Time.deltaTime;
                // 애니메이션은 달리기로
                AniControl(false, false, true);
                // 스피드변경
                PlayerMove.speed = 6.0f;
            }
            else
            {
                // 애니메이션은 걷기로
                AniControl(false, true, false);
                // 스피드변경
                PlayerMove.speed = 3.0f;
            }
        }
        else
        {
            // 키값을 안눌렀을 경우 대기애니메이션
            _PlayerMove.SoundStopPlayType(0);
            _PlayerMove.SoundStopPlayType(1);
            AniControl(true, false, false);
        }
    }

    // 애니메이션 대기, 걷기, 뛰기
    void AniControl(bool idle, bool walk, bool run)
    {
        _PlayerAni.SetBool("Idle", idle);
        _PlayerAni.SetBool("Walk", walk);
        _PlayerAni.SetBool("Run", run);
    }
}
