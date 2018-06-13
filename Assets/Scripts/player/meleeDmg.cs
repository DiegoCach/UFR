using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class meleeDmg : NetworkBehaviour {

    public float dmg;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        var script = other.gameObject.GetComponent<EffectsRobots>();
        var script2 = transform.parent.gameObject.GetComponent<Attack>();
        dmg = script2.dmg;

        if ( int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 15)
        {
            GameManager.init.player1Hp -= dmg;
            StartCoroutine(TakeDmg(other.GetComponent<Animator>(), other.gameObject));
        } else if (int.Parse(other.GetComponent<NetworkIdentity>().netId.ToString()) == 16)
        {
            GameManager.init.player2Hp -= dmg;
            StartCoroutine(TakeDmg(other.GetComponent<Animator>(), other.gameObject));
        }

        switch (other.gameObject.name)
        {
            case "Robotin1":
                script.robot1 = true;
                break;
            case "Robotin2":
                script.robot2 = true;
                break;
            case "Robotin3":
                script.robot3 = true;
                break;
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
