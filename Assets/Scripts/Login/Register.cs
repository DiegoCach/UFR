using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using APIMethods;

public class Register : MonoBehaviour 
{
	protected InputField userPlace;
	protected InputField passPlace;
	protected InputField repeatpassPlace;
	protected InputField emailPlace;
	protected Text message;
	public GameObject loading;
	public GameObject messageHolder;

	protected string parameters;

	// Use this for initialization
	void Start ()
	{
		userPlace = GameObject.Find ("userPlace").GetComponent<InputField> ();
		passPlace = GameObject.Find ("passPlace").GetComponent<InputField> ();
		emailPlace = GameObject.Find ("emailPlace").GetComponent<InputField> ();
		repeatpassPlace = GameObject.Find ("repeatPassPlace").GetComponent<InputField> ();
	}
		
	public void Regist ()
	{
		if (userPlace.text != "" && emailPlace.text != "" && repeatpassPlace.text != ""  && passPlace.text != ""
			&& repeatpassPlace.text != " "  && passPlace.text != " ") {
			if (passPlace.text == repeatpassPlace.text) {

				loading.SetActive (true);
				parameters = "name=" + userPlace.text +
					"&email=" + emailPlace.text + 
					"&pass=" + passPlace.text +
					"&victories=0&defeats=0&urlPhoto=http://h2744356.stratoserver.net/sapiens/robotsGameApi/public/assets/img/user.png";

				StartCoroutine (
					APIHelper.PostRequest (
						"user/create.json",
						parameters
					)
				);

			} else {
				messageHolder.SetActive (true);
				message = GameObject.Find ("message").GetComponent<Text> ();
				message.text = "Las contraseñas no coinciden";
			}
		} else {
			messageHolder.SetActive (true);
			message = GameObject.Find ("message").GetComponent<Text> ();
			message.text = "No puede haber campos vacios";
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
	//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	public void nextlvl()
	{
		int lvl = SceneManager.GetActiveScene ().buildIndex;

		if(lvl < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene (lvl+1);
		}
	}

}
