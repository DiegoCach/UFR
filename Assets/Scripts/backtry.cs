using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using APIMethods;

public class backtry : MonoBehaviour {


	public void back()
	{
		if(SceneManager.GetActiveScene().buildIndex == 1 && APIHelper.requestState == APIHelper.State.successful){
			ChangeScene (0);
		}
		if(SceneManager.GetActiveScene().buildIndex == 2 && APIHelper.requestState == APIHelper.State.successful){
			ChangeScene (7);
		}
		if(SceneManager.GetActiveScene().buildIndex == 4 && APIHelper.requestState == APIHelper.State.successful){
			//ChangeScene (0);
		}
		if(SceneManager.GetActiveScene().buildIndex == 7 && APIHelper.requestState == APIHelper.State.successful){
			ChangeScene (0);
		}
		APIHelper.requestState = APIHelper.State.finished;

		gameObject.SetActive (false);
	}

	public void ChangeScene (int scene)
	{
		SceneManager.LoadScene(scene);
	}
}
