using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuExitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitMenu()
	{
		//falla al volver a entrar: destruir Don't Destroy Load
		SceneManager.LoadScene(0);
		GameObject.Find("NetworkController").SetActive(false);
	}

	public void Continue()
	{
		GameManager.init.exit = true;
		Destroy (GameManager.init.menuExit);
	}
}
