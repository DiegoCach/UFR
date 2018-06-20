using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songRayos : MonoBehaviour {
    public AudioSource audio;
    public float timeAudio;
    bool song;
    // Use this for initialization
    void Start () {
        song = true;
	}
	
	// Update is called once per frame
	void Update () {
        timeAudio += Time.deltaTime;

        if (timeAudio >= 0 && song)
        {
            song = false;
            audio.Play();
        }
        if (timeAudio >= 2)
        {
            audio.Stop();
            timeAudio = 0;
            song = true;
        }
    }
}
