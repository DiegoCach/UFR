using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieza {

	public enum Rarities
	{
		Normal, LessCommon, Rare, Epic, Legendary
	}

	public enum Elements
	{
		Fire, Water, Ray, Earth, Any
	}

	public enum Places
	{
		LeftArm, RightArm, Legs, Head
	}

	public enum Colors
	{
		Default, Blue, Pink, Black, White, Purple, Brown, Yellow, Red, Green
	}

	public string name;
	public string description;
	public Places place;
	public Elements element;
	public Rarities rarity;
	public Colors color;
	public int life;
	public int damage;
	public int speed;
	public int cadence;

	public Pieza (string name, string description, Rarities rarity, Places place, Elements element,
		Colors color, int life, int damage, int speed, int cadence){
		this.name = name;
		this.description = description;
		this.place = place;
		this.element = element;
		this.rarity = rarity;
		this.color = color;
		this.life = life;
		this.damage = damage;
		this.speed = speed;
		this.cadence = cadence;
	}
}
