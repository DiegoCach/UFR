using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBombilla : MonoBehaviour 
{
	public bool activado = false;


	// Update is called once per frame
	void Update () 
	{
		if (activado == true) 
		{
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			activado = false;
		}
		
	}
}
