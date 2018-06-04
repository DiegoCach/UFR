using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using APIMethods;

public class ForgotPass : MonoBehaviour {

	protected InputField passPlace;
	protected InputField repeatpassPlace;
	protected Text message;
	public GameObject loading;
	public GameObject messageHolder;

	protected string parameters;

	// Use this for initialization
	void Start ()
	{
		passPlace = GameObject.Find ("passPlace").GetComponent<InputField> ();
		repeatpassPlace = GameObject.Find ("repeatPassPlace").GetComponent<InputField> ();
	}

	public void ChangePass ()
	{
		if ((passPlace.text == repeatpassPlace.text) && repeatpassPlace.text != "" && passPlace.text != "") {

			loading.SetActive (true);
			parameters = "pass=" + passPlace.text;

			StartCoroutine (
				APIHelper.PostRequest(
					"user/editPass.json",
					parameters
				)
			);

		} else {
			messageHolder.SetActive (true);
			message = GameObject.Find ("message").GetComponent<Text> ();
			message.text = "Las contraseñas no coinciden";
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
