using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongCaminar : MonoBehaviour {

    public AudioSource song;
    public AudioClip caminar;
    float timeAudio;
    bool caminando;
    // Use this for initialization
    void Start()
    {
        song.clip = caminar;
    }

    // Update is called once per frame
    void Update()
    {
        timeAudio += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
        {
            song.Play();
            timeAudio = 0;
            Debug.Log("play");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            song.Stop();
        }


    }
}
