using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagPieces : MonoBehaviour {

	public Object[] leftArms;
	public Object[] rightArms;
	public Object[] legs;
	public Object[] heads;
	public Object[] chests;

	// Use this for initialization
	void Awake () {
		leftArms = Resources.LoadAll("data/leftArms");
		rightArms = Resources.LoadAll("data/rightArms");
		legs = Resources.LoadAll("data/legs");
		heads = Resources.LoadAll("data/heads");
		chests = Resources.LoadAll("data/chests");
	}

	/*void LoadPieceInfo(string path, string bodyPart, string infoName){
		var piece = GameObject.Find (bodyPart);

		piece.GetComponent<PieceInfo> ().pieceData = (Piece) Resources.Load ("data/" + path + "/"+ infoName);

		//piece.GetComponent<MeshFilter> ().mesh = piece.GetComponent<PieceInfo> ().pieceData.skin;
		//piece.GetComponent<MeshCollider> ().sharedMesh = piece.GetComponent<PieceInfo> ().pieceData.skin;
	}*/
}
