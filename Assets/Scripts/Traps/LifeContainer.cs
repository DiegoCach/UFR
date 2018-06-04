using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LifeContainer : NetworkBehaviour{

    public int hit = 0;
    private int playerHp;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.GetComponent<NetworkIdentity>().hasAuthority == true && coll.gameObject.name == "Player1")
        {
            Debug.Log("dañoP1");
            playerHp = 1;
            CmdHeal(20);
        }
        if (coll.gameObject.GetComponent<NetworkIdentity>().hasAuthority == false && coll.gameObject.name == "Player2")
        {
            Debug.Log("dañoP2");
            playerHp = 2;
            CmdHeal(20);
        }
    }

    [Command]
    void CmdHeal(float amount)
    {
        if (playerHp == 1)
        {
            GameManager.init.player1Hp += amount;
        }
        else if (playerHp == 2)
        {
            GameManager.init.player2Hp += amount;
        }
        Destroy(gameObject,0);
    }

}
