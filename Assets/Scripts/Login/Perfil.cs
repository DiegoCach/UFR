using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using APIMethods;
using System.IO;

public class Perfil : MonoBehaviour {

	protected string info;
	protected JSONDataUser data;
	protected Text nombre;
	protected Text victorias;
	protected Text derrotas;
	protected Image imageProfile;

	void Awake(){
		info = PlayerPrefs.GetString ("data");

		data = JSONHelper.JSONToDataUser (info);
	}

	public void GoToTest ()
	{
		SceneManager.LoadScene("test");
	}

	// Use this for initialization
	void Start () {
		nombre = GameObject.Find ("nombre").GetComponent<Text>();
		victorias = GameObject.Find ("victorias").GetComponent<Text>();
		derrotas = GameObject.Find ("derrotas").GetComponent<Text>();
		imageProfile = GameObject.Find ("imgProfile").GetComponent<Image>();

		nombre.text = data.name;
		victorias.text = "Victorias: " + data.victories;
		derrotas.text = "Partidas jugadas: " + (data.defeats + data.victories);
		//Sprite imgPro = File.ReadAllLines(data.urlPhoto) as Sprite;

		if(data.urlPhoto != null && data.urlPhoto != ""){
			Debug.Log (PlayerPrefs.GetString("urlPhoto"));
			StartCoroutine (LoadPhoto (data.urlPhoto));
		}
		//imageProfile.texture = LoadPNG(data.urlPhoto);
	}

	IEnumerator LoadPhoto(string url) {
		WWW www = new WWW(url);
		yield return www;
		if(www.isDone)
			imageProfile.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
	}
}
