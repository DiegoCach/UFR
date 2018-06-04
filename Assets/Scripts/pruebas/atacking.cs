using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacking : MonoBehaviour 
{
	private bool BvsB=false;
	private bool shooterI=true;
	private bool shooterR=false;
	private bool rechargePass;
	public GameObject prefab;
	public Transform spawnI;
	public Transform spawnR;
	public float forceAtack;
	private float timeT=0.05f;
	private float timeA;
	private int bullet;
	public int timeRechange;
	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Mouse0)&&BvsB) 																
		{
			//animacion
		}
		if (Input.GetButtonDown("Fire1")&&shooterI) 																
		{
			timeA += Time.deltaTime;
			Shooter (spawnI);
			//animacion
		}
		if (Input.GetButtonDown("Fire2")&& shooterR && rechargePass==false) 																
		{
			timeA += Time.deltaTime;
			Shooter (spawnR);
			//animacion
		}

		rechargeBullet ();
	}

	public void Shooter(Transform arm)
	{
		if(timeA>=timeT)
		{
			GameObject clon = (GameObject)Instantiate (prefab, arm.position, Quaternion.identity);
			clon.GetComponent<Rigidbody> ().AddForce (arm.transform.forward *forceAtack, ForceMode.Impulse);
			timeA = 0;
			bullet--;
			Destroy (clon, 5);
			//animacion
		}
	}

	private void rechargeBullet()
	{
		if(bullet<=0)
		{
			rechargePass = true;
			StartCoroutine (recharge());
		}
	}

	IEnumerator recharge()
	{
		yield return new WaitForSeconds(timeRechange);
		bullet = 30;//numero del gameControler
	}
}
