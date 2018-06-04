using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanSlots : MonoBehaviour {

	public GameObject slot;
	public GameObject inventory;
	ManagPieces controller;
	Dropdown dropdown;

	Color unique = new Color (255, 132, 51);
	Color legendary = new Color (255, 249, 0);
	Color normal = Color.white;
	Color rare = new Color (0, 202, 255);
	Color epic = new Color (255, 47, 215);

	void Awake () {
		dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
		inventory = GameObject.Find ("Inventory");
		controller = GetComponent<ManagPieces> ();
	}

	// Use this for initialization
	void Start () {
		

		for(int i = 0; i < controller.legs.Length; i++){
			var invSlot = Instantiate (slot, inventory.transform);
			invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.legs [i];
			invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
			if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
				invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
			}
		}


	}

	public Color RarityColor(Piece piece){
		Color slotColor = new Color (0,0,0,100);
		if(piece.rarity == Piece.Rarities.Unique){
			slotColor = unique;
		}
		if(piece.rarity == Piece.Rarities.Legendary){
			slotColor = legendary;
		}
		if(piece.rarity == Piece.Rarities.Epic){
			slotColor = epic;
		}
		if(piece.rarity == Piece.Rarities.Rare){
			slotColor = rare;
		}
		if(piece.rarity == Piece.Rarities.Normal){
			slotColor = normal;
		}
		print (piece.rarity.ToString() + ": "+slotColor);
		return slotColor;
	}

	public void changeSlots(){
		foreach(Transform child in inventory.transform){
			Destroy (child.gameObject);	
		}
		if (dropdown.value == 0) {
			for(int i = 0; i < controller.legs.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.legs [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 1) {
			for(int i = 0; i < controller.leftArms.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.leftArms [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 2) {
			for(int i = 0; i < controller.rightArms.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.rightArms [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 3) {
			for(int i = 0; i < controller.heads.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.heads [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 4) {
			for(int i = 0; i < controller.chests.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.chests [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().color = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
