using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour {

	Piezas piezas;

	// Use this for initialization
	void Start () {
		
		piezas = GetComponent<Piezas> ();
		GetLeftArms ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetLeftArms()
	{
		for(int i = 0; i < piezas.leftArms.Count; i++)
		{
			Debug.Log (piezas.leftArms[i].name);
		}
	}

	public void GetRightArms()
	{
		for(int i = 0; i < piezas.rightArms.Count; i++)
		{
			Debug.Log (piezas.rightArms [i].name);
		}
	}

	public void GetLegs()
	{
		for(int i = 0; i < piezas.legs.Count; i++)
		{
			Debug.Log (piezas.legs[i].name);
		}
	}

	public void GetHeads()
	{
		for(int i = 0; i < piezas.heads.Count; i++)
		{
			Debug.Log (piezas.heads[i].name);
		}
	}
}
