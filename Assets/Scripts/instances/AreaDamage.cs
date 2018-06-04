using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {

	public float damage;
	// Use this for initialization
	void Start () 
	{
		damage = GameManager.init.damageArea;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag=="enemigo")
		{
			coll.gameObject.GetComponent<Life> ().hp-=damage;
			coll.gameObject.GetComponent<MovimientoPersonaje> ().speed -= GameManager.init.slowDawnVelocity;
		}
	}
}
