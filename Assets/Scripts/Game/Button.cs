using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    public Text winner;
    public GameObject J1, J2;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (GameManager.init.player1Hp < GameManager.init.player2Hp)
        {
            winner.text = "Player 2 Wins this round!";
            J2.SetActive(true);

        } else if (GameManager.init.player2Hp < GameManager.init.player1Hp)
        {
            winner.text = "Player 1 Wins this round!";
            J1.SetActive(true);
        }

        if (winner.text == "Player 1 Wins this round!" || winner.text == "Player 2 Wins this round!")
        { 
            StartCoroutine("Restart");
        }
	}

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        GameManager.init.restart();
        J1.SetActive(false);
        J2.SetActive(false);
    }

}
