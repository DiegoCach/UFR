using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceInfo : MonoBehaviour {

	float damageT;
	float lifeT;
	float velocityT;
	float cadenceT;

	public Piece pieceData;
	string bodyName;
	int valor = 0;

	void Start () {

		if(pieceData != null){

		}

	}

	public void DisplayInfo(){
		CombatPieces combatPieces = GameObject.Find("CombatPieces").GetComponent<CombatPieces> ();

		Dropdown dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
		if (dropdown.value == 0) {
			bodyName = "legs";
			combatPieces.legs = pieceData;
		}
		if (dropdown.value == 1) {
			bodyName = "leftArm";
			combatPieces.leftArm = pieceData;
		}
		if (dropdown.value == 2) {
			bodyName = "rightArm";
			combatPieces.rightArm = pieceData;
		}
		if (dropdown.value == 3) {
			bodyName = "head";
			combatPieces.head = pieceData;
		}
		if (dropdown.value == 4) {
			bodyName = "chest";
			combatPieces.chest = pieceData;
		}

		for(int i = 0; i< pieceData.skins.Length; i++){
			GameObject bodyPart = GameObject.Find (pieceData.skins[i].name);
			if(bodyPart.transform.childCount > 0){
				foreach(Transform child in bodyPart.transform){
					Destroy (child.gameObject);	
				}
			}
			GameObject part = Instantiate (pieceData.skins[i], bodyPart.transform.position, bodyPart.transform.rotation);
			part.transform.parent = bodyPart.transform;
			part.transform.localScale = pieceData.skins[i].transform.lossyScale;
		}

		//GameObject bodyPart = GameObject.Find (bodyName);
		/*var arr = Instantiate (pieceData.skin, bodyPart.transform.position, bodyPart.transform.rotation);
		arr.transform.SetParent (bodyPart.transform);
		arr.transform.localScale = pieceData.skin.transform.localScale;*/


		//bodyPart.GetComponent<MeshFilter> ().mesh = pieceData.skin;
		//bodyPart.GetComponent<MeshCollider> ().sharedMesh = pieceData.skin;

		GameObject.Find ("rarity").GetComponent<Text> ().text = pieceData.rarity.ToString ();
		GameObject.Find ("damage").GetComponent<Text> ().text = "Daño: " + pieceData.damage.ToString ();
		GameObject.Find ("life").GetComponent<Text> ().text = "Vida: " + pieceData.life.ToString ();
		GameObject.Find ("velocity").GetComponent<Text> ().text = "Velocidad: " + pieceData.speedMovement.ToString ();
		GameObject.Find ("cadence").GetComponent<Text> ().text = "Cadencia: " + pieceData.cadence.ToString ();
		StatsTotal (combatPieces);
	}

	void StatsTotal(CombatPieces cp)
	{
		damageT=0;
		lifeT=0;
		velocityT=0;
		cadenceT=0;

		if (cp != null) {	
			GameObject findR = GameObject.Find ("TypeRobot");	
			Sumar (cp.legs);
			Sumar (cp.leftArm);
			Sumar (cp.rightArm);
			Sumar (cp.head);
			Sumar (cp.chest);
			if (findR.GetComponent<Text> ().text == "Player Defensa") {
				damageT += 2;
				lifeT += 2;
				velocityT += 2;
				cadenceT += 2;
			}
			if(findR.GetComponent<Text> ().text == "Player Velocidad"){
				damageT += 3;
				lifeT += 3;
				velocityT += 3;
				cadenceT += 3;
			}
			if(findR.GetComponent<Text> ().text == "Player Ataque")
			{
				damageT+=1;
				lifeT += 1;
				velocityT += 1;
				cadenceT += 1;
			}
		}

		GameObject.Find ("damageS").GetComponent<Text> ().text = "Daño " + damageT;
		GameObject.Find ("lifeS").GetComponent<Text> ().text = "Vida " + lifeT;
		GameObject.Find ("velocityS").GetComponent<Text> ().text = "Velocidad " + velocityT;
		GameObject.Find ("cadenceS").GetComponent<Text> ().text = "Cacencia " + cadenceT;
	}

	void Sumar(Piece p)
	{
		if (p == null)
			return;
		
		damageT += p.damage;
		lifeT += p.life;
		velocityT += p.speedMovement;
		cadenceT += p.cadence;
	}
}