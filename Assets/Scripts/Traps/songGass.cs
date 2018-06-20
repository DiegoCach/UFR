using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class songGass : MonoBehaviour {
    public AudioSource audio;
    public float timeAudio;
    bool song;
	// Use this for initialization
	void Start () {
        song = true;
    }
	
	// Update is called once per frame
	void Update () {
        timeAudio+=Time.deltaTime;

        if (timeAudio >= 3.5f && song)
        {
            song = false;
            audio.Play();
        }
        if (timeAudio >= 6)
        {
            audio.Stop();
            timeAudio = 0;
            song = true;
        }
	}
}
