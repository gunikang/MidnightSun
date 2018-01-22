using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] MusicClip;
    public AudioSource MusicSource;

    
    void Start ()
    {
        
	}
    void MusicClipObj(int type)
    {
        MusicSource.clip = MusicClip[type];
    }
	
    
	void Update ()
    {
		
	}
}
