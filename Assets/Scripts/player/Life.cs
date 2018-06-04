using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Life : NetworkBehaviour 
{
	public float hp;
	// Use this for initialization
	void Start () 
	{
		//cambia el nombre para que el cliente tenga los nombres correctos para asignar la vida (el Host lo hace en NetworkingController)
	    if(!isServer)
		{
			if (isLocalPlayer && hasAuthority) {
				gameObject.name = "Player1";
			} else {
				gameObject.name = "Player2";
			}
		}

		if (gameObject.name== "Player1") 
		{
			hp = GameManager.init.player1Hp;
		} 
		else if (gameObject.name == "Player2") 
		{
			hp = GameManager.init.player2Hp;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.name == "Player1") 
		{
			hp = GameManager.init.player1Hp;
			//GameManager.init.player1Hp=hp;
			gameOver ();
		} 
		if (gameObject.name == "Player2") 
		{
			hp = GameManager.init.player2Hp;
			//GameManager.init.player2Hp=hp;
			gameOver ();
		}
			
	}

	public void gameOver()
	{
		if (hp <= 0) 
		{
			//para que notification no entren indefinidamente

			if (GameManager.init.player2Hp < GameManager.init.player1Hp) 
			{
				GameManager.init.pl1 = true;
            } 
			if (GameManager.init.player1Hp < GameManager.init.player2Hp) 
			{
				GameManager.init.pl2 = true;
            }
			GameManager.init.viewGameWinLoser();
            Debug.Log("Me muero");
            Debug.Log(this.gameObject);
		}
	}
}
