using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFisicATK : MonoBehaviour 
{
	private float damage;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.tag=="enemigo")
		{
			//quitar vida al que recibe el golpe mediante el valorde Damage
		}
	}
}
