using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRot : MonoBehaviour {

	GameObject center;
	// Use this for initialization
	void Start () {
		center = GameObject.Find ("center");
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.LookAt (center.transform.position);

		gameObject.transform.RotateAround (center.transform.position, center.transform.position, 0.1f);
	}
}
