using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour {

	public GameObject cameraMenu;

	public Image crossfire;
	private bool locked = true;
	// Use this for initialization
	void Awake () {
		cameraMenu = GameObject.Find ("Camera");
		if (cameraMenu != null) {
			Destroy (cameraMenu);
	
		} else {
			cameraMenu = GameObject.Find ("Camera(Clone)");
			if(cameraMenu != null){
				Destroy (cameraMenu);
			}

		}
		crossfire = GameObject.Find ("cross").GetComponent<Image> ();
		Color crossAlpha = crossfire.color;
		crossAlpha.a = 0.5f;

		crossfire.color = crossAlpha;
		Cursor.lockState = CursorLockMode.Locked;

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			locked = !locked;
		if (locked == false) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		} else {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

}
