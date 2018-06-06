using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class EffectsRobots : NetworkBehaviour
{
    public bool robot1 = false, robot2 = false, robot3 = false;
    public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (robot1 == true)
        {
            RobotOne(Player.transform.position, 3f);
        }
        if (robot2 == true)
        {

        }
        if (robot3 == true)
        {

        }
	}

    private void RobotOne(Vector3 PosicionHito, float Velocidad)
    {
        if (gameObject.GetComponent<patron>().isActiveAndEnabled == true)
        {
            gameObject.GetComponent<patron>().gameObject.SetActive(false);
        }
        Vector3 VectorHaciaObjetivo = PosicionHito - transform.position;

        VectorHaciaObjetivo.Normalize();
        VectorHaciaObjetivo *= Velocidad;
        VectorHaciaObjetivo = new Vector3(VectorHaciaObjetivo.x,
                                          VectorHaciaObjetivo.y,
                                          VectorHaciaObjetivo.z);

        transform.Translate(VectorHaciaObjetivo * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(VectorHaciaObjetivo);
    }
}
