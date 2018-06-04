using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIMethods;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene (int scene)
	{
		APIHelper.requestState = APIHelper.State.finished;
		SceneManager.LoadScene(scene);
	}
}
