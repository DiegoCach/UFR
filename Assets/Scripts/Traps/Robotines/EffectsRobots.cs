using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityEngine;

public class EffectsRobots : NetworkBehaviour
{
    public bool robot1 = false, robot2 = false, robot3 = false;
    private GameObject Player , Player2;
	public float velRot;
	public GameObject robotHijo;
	public GameObject tasmania;
	bool rotar;

	public float tiempo = 5;
	public float cont =0;
	// Use this for initialization
	void Start () {
		rotar = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(rotar)
		{
			robotHijo.transform.Rotate(Vector3.forward * Time.deltaTime * velRot);
		}
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
			RobotThree(Player.transform, 3f);
        }
        if (robot3 == true)
        {
            
        }
	}

    private void RobotOne()
    {
        if (gameObject.GetComponent<patron>().isActiveAndEnabled == true)
        {
            gameObject.GetComponent<patron>().enabled = false;
			Debug.Log("if");
			rotar = true;
			GameObject clon=Instantiate(tasmania,robotHijo.transform);
			clon.transform.Rotate (new Vector3 (90, 0, 0));
			clon.transform.localPosition=new Vector3 (0, 0,-0.3f);

			cont = 0;
        }

		//GetComponent<NavMeshAgent>().destination = Player.transform.position;

		Vector3 dirRobot = (Player.transform.position - this.transform.position).normalized * 3;
		this.transform.Translate (dirRobot*Time.deltaTime, Space.World);

		cont += Time.deltaTime;

		if (cont > tiempo) {
			gameObject.GetComponent<patron>().enabled = true;
			Debug.Log ("hola estos son mis hijos");
			Debug.Log (robotHijo.transform.childCount);

			if (robotHijo.transform.childCount >= 1)
			{
				Destroy (robotHijo.transform.GetChild (0).gameObject);
			}
			robot1 = false;
			rotar = false;
			robotHijo.transform.localRotation = Quaternion.Euler (-90, 0, 0);
			//GetComponent<NavMeshAgent> ().SetDestination (this.transform.position);
		}
    }

    private void RobotThree(Transform PosicionHito, float Velocidad)
    {
        if (gameObject.GetComponent<patron>().isActiveAndEnabled == true)
        {
            gameObject.GetComponent<patron>().enabled = false;
			cont = 0;
        }
		transform.LookAt(Player.transform);

		cont += Time.deltaTime;

		RaycastHit hit;
		if (Physics.Raycast(gameObject.transform.position, Player.transform.position, out hit, 100))
		{
			string UIdentity = hit.collider.gameObject.name;

		}

    }
}
