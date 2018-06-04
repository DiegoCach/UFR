using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using APIMethods;

public class Login : MonoBehaviour 
{
	protected InputField userPlace;
	protected InputField passPlace;
	public GameObject loading;
	public GameObject messageHolder;
	protected Text message;

	protected string parameters;

	void Start ()
	{
		userPlace = GameObject.Find ("userPlace").GetComponent<InputField> ();
		passPlace = GameObject.Find ("passPlace").GetComponent<InputField> ();
	}

	public void Log ()
	{
		if (userPlace.text != "" && passPlace.text != "") 
		{
			loading.SetActive (true);
			parameters = "name=" + userPlace.text +
			"&pass=" + passPlace.text;

			StartCoroutine (
				APIHelper.LoginRequest (
					"user/login.json",
					parameters
				)
			);
		}
		else 
		{
			messageHolder.SetActive (true);
			message = GameObject.Find ("message").GetComponent<Text> ();
			message.text = "No pueden haber campos vacios";
		}
	}

	void Update()
	{
		if (APIHelper.requestState == APIHelper.State.successful) {
			loading.SetActive (false);
			ChangeScene (3);
		}
		else if (APIHelper.requestState == APIHelper.State.aborted || APIHelper.requestState == APIHelper.State.error)
		{
			loading.SetActive (false);
			messageHolder.SetActive (true);
			message = GameObject.Find ("message").GetComponent<Text> ();
			message.text = APIHelper.response.message;
			APIHelper.requestState = APIHelper.State.finished;
		}
	}

	public void AppExit(){
		Application.Quit ();
	}

	public void ChangeScene (int scene)
	{
		APIHelper.requestState = APIHelper.State.finished;
		SceneManager.LoadScene(scene);
	}
}
