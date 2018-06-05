using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class meleeDmg : NetworkBehaviour {


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ( int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 10)
        {
            GameManager.init.player1Hp--;
            StartCoroutine(TakeDmg(other.GetComponent<Animator>(), other.gameObject));
        } else if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 11)
        {
            GameManager.init.player2Hp--;
            StartCoroutine(TakeDmg(other.GetComponent<Animator>(), other.gameObject));
        }
    }


    IEnumerator TakeDmg(Animator anim, GameObject playerDmg)
    {
        anim.SetTrigger("BlendDmg");
        playerDmg.GetComponent<MovimientoPersonaje>().enabled = false;
        playerDmg.GetComponent<Attack>().enabled = false;
        playerDmg.GetComponent<Abilities>().enabled = false;
        playerDmg.GetComponent<CapsuleCollider>().enabled = false;
        playerDmg.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(2f);
        playerDmg.GetComponent<MovimientoPersonaje>().enabled = true;
        playerDmg.GetComponent<Attack>().enabled = true;
        playerDmg.GetComponent<Abilities>().enabled = true;
        playerDmg.GetComponent<CharacterController>().enabled = true;
    }
}
