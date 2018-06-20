using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SongController : MonoBehaviour {

	public AudioMixer audioController;
	public Scrollbar[] scrolls;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void volumenCancion(){
		audioController.SetFloat ("cancion", (scrolls [1].value)*20);
		Debug.Log (scrolls [1].value);

	}
	public void volumenEfectos(){
		audioController.SetFloat ("efectos", (scrolls [2].value)*20);


	}

	public void volumenGeneral(){
		audioController.SetFloat ("master", (scrolls [0].value)*20);


	}
}
