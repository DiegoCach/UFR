using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bullet : NetworkBehaviour 
{
	public float damage;
	private int playerHp;
	// Use this for initialization
	void Start () 
	{
		damage = GameManager.init.damageBullet;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider coll)
	{

		if(!isServer)
		{
			return;
		}
		
		if (coll.gameObject.name == "Player1")
		{
			Debug.Log ("dañoP1");
			//coll.gameObject.GetComponent<Life> ().hp -= damage;
			playerHp = 1;
			RpcDamage (damage);
		}
		if (coll.gameObject.name == "Player2") 
		{
			Debug.Log ("dañoP2");
			//coll.gameObject.GetComponent<Life> ().hp -= damage;
			playerHp = 2;
			RpcDamage (damage);
		} 
	}

	[ClientRpc]
	void RpcDamage(float amount)
	{
		if (playerHp == 1) {
			GameManager.init.player1Hp-=damage;
		} else if (playerHp == 2) {
			GameManager.init.player2Hp-=damage;
		}
		Debug.Log("daño recibido: " + amount);
	}
}
