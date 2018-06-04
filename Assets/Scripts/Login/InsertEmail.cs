using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using APIMethods;

public class InsertEmail : MonoBehaviour 
{
	protected InputField userPlace;
	protected InputField emailPlace;
	protected Text message;
	public GameObject loading;
	public GameObject messageHolder;
	protected string parameters;

	void Start ()
	{
		userPlace = GameObject.Find ("userPlace").GetComponent<InputField> ();
		emailPlace = GameObject.Find ("emailPlace").GetComponent<InputField> ();
	}

	public void Check ()
	{
		if (userPlace.text != "" && emailPlace.text != "") 
		{
			loading.SetActive (true);
			parameters = "name=" + userPlace.text +
				"&email=" + emailPlace.text;

			StartCoroutine (
				APIHelper.GetRequest (
					"user/checkToRecoverPass.json",
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
			messageHolder.SetActive (true);
			message = GameObject.Find ("message").GetComponent<Text> ();
			message.text = APIHelper.response.message;
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

	public void ChangeScene (int scene)
	{
		APIHelper.requestState = APIHelper.State.finished;
		SceneManager.LoadScene(scene);
	}
}
