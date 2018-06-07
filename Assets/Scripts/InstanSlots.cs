using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanSlots : MonoBehaviour {

	public GameObject slot;
	public GameObject inventory;
	ManagPieces controller;
	Dropdown dropdown;

    public Sprite unique, legendary, epic, rare, normal;

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
			invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
			if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
				invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
			}
		}


	}

	public Sprite RarityColor(Piece piece){
		Sprite slotColor = unique;
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
        Debug.Log(slotColor);
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
				invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 1) {
			for(int i = 0; i < controller.leftArms.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.leftArms [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 2) {
			for(int i = 0; i < controller.rightArms.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.rightArms [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 3) {
			for(int i = 0; i < controller.heads.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.heads [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
				if(invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon != null){
					invSlot.transform.GetChild (1).GetComponent<Image> ().sprite = invSlot.GetComponent<PieceInfo> ().pieceData.inventoryIcon;
				}
			}
		}
		if (dropdown.value == 4) {
			for(int i = 0; i < controller.chests.Length; i++){
				var invSlot = Instantiate (slot, inventory.transform);
				invSlot.GetComponent<PieceInfo> ().pieceData = (Piece) controller.chests [i];
				invSlot.transform.GetChild (0).GetComponent<Image> ().sprite = RarityColor (invSlot.GetComponent<PieceInfo> ().pieceData);
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
