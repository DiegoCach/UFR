using UnityEngine;
using System;

public class DataModel: ScriptableObject {

	public int damage, life, speedMovement, cadence;
	public string type, description;

	public void Load (string line){
		string[] elements = line.Split(',');
		if(elements.Length == 7){
			name = elements[0];
			type = elements[1];
			damage = Convert.ToInt32(elements[2]);
			life = Convert.ToInt32(elements[3]);
			speedMovement = Convert.ToInt32(elements[4]);
			cadence = Convert.ToInt32(elements[5]);
			description = elements[6];
		}
	}
}
