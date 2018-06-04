using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPice : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.parent.position;
		//Debug.Log(transform.parent.position);
	}
}
