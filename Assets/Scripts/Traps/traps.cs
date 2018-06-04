using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class traps : NetworkBehaviour
{

    private float time;
    MovimientoPersonaje mov;
    MovimientoPersonaje mov2;
    private ParticleSystem part;

    void Start () {
        part = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (mov == null)
        {
            mov = GameObject.Find("Player1").GetComponent<MovimientoPersonaje>();
            mov2 = GameObject.Find("Player2").GetComponent<MovimientoPersonaje>();
        }
        time += Time.deltaTime;
        if (time >= 3)
        {
            if(!part.isPlaying)
            {
                part.Play();
            }
        }
        if (time >= 6)
        {
            time = 0;
            part.Stop();
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player1" && time >= 3)
        {
            mov.slow = true;
        } else if (other.name == "Player2" && time >= 3)
        {
            mov2.slow = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player1" )
        {
            mov.slow = false;
        }
        else if (other.name == "Player2")
        {
            mov2.slow = false;
        }
    }
}
