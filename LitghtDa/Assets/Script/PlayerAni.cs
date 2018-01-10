using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    private Animator _PlayerAni = null;

	void Start ()
    {
        _PlayerAni = GetComponent<Animator>();
	}
	
	void Update ()
    {
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                AniControl(false, false, true);
                PlayerMove.speed = 6.0f;
            }
            else
            {
                AniControl(false, true, false);
                PlayerMove.speed = 3.0f;
            }
        }
        else
            AniControl(true, false, false);
    }

    void AniControl(bool idle, bool walk, bool run)
    {
        _PlayerAni.SetBool("Idle", idle);
        _PlayerAni.SetBool("Walk", walk);
        _PlayerAni.SetBool("Run", run);
    }
}
