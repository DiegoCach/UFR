using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using APIMethods;

public class changePhoto : MonoBehaviour {

	protected Text message;
	public GameObject loading;
	public GameObject messageHolder;
	protected Image imageProfile;
	protected string parameters;

	// Use this for initialization
	void Start ()
	{
		imageProfile = GameObject.Find ("imgProfile").GetComponent<Image>();
	}

	public void ChangePhoto (string photoName)
	{
		loading.SetActive (true);
		parameters = "urlPhoto=" + photoName;

		StartCoroutine (
			APIHelper.PostPhoto(
				"user/editPhoto.json",
				parameters
			)
		);



		
	}

	void Update()
	{
		//Debug.Log (APIHelper.requestState);
		if (APIHelper.requestState == APIHelper.State.successful) {
			loading.SetActive (false);
			StartCoroutine (LoadPhoto (PlayerPrefs.GetString ("urlPhoto")));
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

	IEnumerator LoadPhoto(string url) {
		WWW www = new WWW(url);
		yield return www;
		if (www.isDone) {
			imageProfile.sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0, 0));
		}
	}
}
