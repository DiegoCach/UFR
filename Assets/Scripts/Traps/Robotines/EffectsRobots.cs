using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityEngine;

public class EffectsRobots : NetworkBehaviour
{
    public bool robot1 = false, robot2 = false, robot3 = false;
    private GameObject Player , Player2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Player == null)
        {
            Player = GameObject.Find("Player1");
        }
        if (Player2 == null)
        {
            Player2 = GameObject.Find("Player2");
        }
        if (robot1 == true)
        {
            RobotOne();
        }
        if (robot2 == true)
        {

        }
        if (robot3 == true)
        {
            RobotThree(Player.transform, 3f);
        }
	}

    private void RobotOne()
    {
        if (gameObject.GetComponent<patron>().isActiveAndEnabled == true)
        {
            gameObject.GetComponent<patron>().enabled = false;
        }
        GetComponent<NavMeshAgent>().destination = Player.transform.position;
    }

    private void RobotThree(Transform PosicionHito, float Velocidad)
    {
        if (gameObject.GetComponent<patron>().isActiveAndEnabled == true)
        {
            gameObject.GetComponent<patron>().enabled = false;
        }
        transform.LookAt(PosicionHito);
    }
}
