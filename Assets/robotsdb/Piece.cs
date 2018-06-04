using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Piece")]
public class Piece: ScriptableObject {
	public enum Rarities {
		Any,Normal,Rare,Epic,Legendary,Unique
	}

	public enum BodySides {
		Any,Aura,Base,Chest,LeftArm,RightArm,Head,Legs,
		LeftWeapon,RightWeapon,CosmeticBack,CosmeticChest,
		CosmeticLeftArm,CosmeticRightArm,CosmeticHead,CosmeticLegs
	}
	public Rarities rarity;
	public BodySides bodySide;

	public int damage, life, speedMovement, cadence;
	public string description;
	public GameObject[] skins;
	public Sprite inventoryIcon;

	public void Load (string line){
		string[] elements = line.Split(',');
		if (elements.Length == (7 + 1)) {
			name = elements [0];
			bodySide = (BodySides) System.Enum.Parse(typeof(BodySides), elements [1]);
			rarity = (Rarities) System.Enum.Parse(typeof(Rarities), elements [2]);
			damage = Convert.ToInt32 (elements [3]);
			life = Convert.ToInt32 (elements [4]);
			speedMovement = Convert.ToInt32 (elements [5]);
			cadence = Convert.ToInt32 (elements [6]);
			description = elements [7];
		}
	}

	public Piece(){}
	public Piece init (BodySides bodySide, Rarities rarity,
		int damage, int life, int speedMovement, 
		int cadence, string description){
		this.name = name;
		this.damage = damage;
		this.life = life;
		this.speedMovement = speedMovement;
		this.cadence = cadence;
		this.bodySide = bodySide;
		this.rarity = rarity;
		this.description = description;
		return this;
	}
}
