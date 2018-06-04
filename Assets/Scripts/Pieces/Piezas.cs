using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : MonoBehaviour {
	
	public List<List<Pieza>> allPieces = new List<List<Pieza>>();

	public List<Pieza> leftArms = new List<Pieza>();
	public List<Pieza> rightArms = new List<Pieza>();
	public List<Pieza> heads = new List<Pieza>();
	public List<Pieza> legs = new List<Pieza>();

	public const string DEFENSE_HEAD = "HD_";
	public const string AGILITY_HEAD = "HA_";
	public const string STRENGTH_HEAD = "HS_";

	public const string DEFENSE_LEGS = "LGD_";
	public const string AGILITY_LEGS = "LGA_";
	public const string STRENGTH_LEGS = "LGS_";

	public const string DEFENSE_LEFT_ARM = "LAD_";
	public const string AGILITY_LEFT_ARM = "LAA_";
	public const string STRENGTH_LEFT_ARM = "LAS_";

	public const string AGILITY_RIGHT_ARM = "RAA_";
	public const string DEFENSE_RIGHT_ARM = "RAD_";
	public const string STRENGTH_RIGHT_ARM = "RAS_";

	// Use this for initialization
	void Awake () {

		heads.Add (new Pieza(DEFENSE_HEAD + "001", 
			"Un gran casco para un gran robot",
			Pieza.Rarities.LessCommon,
			Pieza.Places.Head,
			Pieza.Elements.Any,
			Pieza.Colors.Default,
			6, 2, 2, 2
		));

		leftArms.Add (new Pieza(STRENGTH_LEFT_ARM + "001", 
			"Brazo diseñado para el combate",
			Pieza.Rarities.Normal,
			Pieza.Places.LeftArm,
			Pieza.Elements.Any,
			Pieza.Colors.Default,
			1, 4, 1, 2
		));

		rightArms.Add (new Pieza(DEFENSE_RIGHT_ARM + "001", 
			"Un brazo robusto, ideal para la defensa",
			Pieza.Rarities.Normal,
			Pieza.Places.RightArm,
			Pieza.Elements.Any,
			Pieza.Colors.Default,
			4, 1, 1, 2
		));

		legs.Add (new Pieza(AGILITY_LEGS + "001", 
			"Piernas agiles",
			Pieza.Rarities.Normal,
			Pieza.Places.Legs,
			Pieza.Elements.Any,
			Pieza.Colors.Default,
			1, 2, 4, 1
		));
	}
		
}
