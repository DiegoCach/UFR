using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ChangeScenes :NetworkBehaviour {

	int num = 1;
	// Use this for initialization
	void Start () {
		TypeRobot ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene (int scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void ChangeSceneInventary (int scene)
	{
		CombatPieces find=GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ();
		if(find.head != null && find.chest != null && find.legs != null)
		{
			SceneManager.LoadScene(scene);
		}
	}

	public void AppExit()
	{
		Application.Quit ();
	}

	public void TypeRobot()
	{
		GameObject findR = GameObject.Find ("TypeRobot");
		num++;
		if (num == 1) {
			findR.GetComponent<Text> ().text = "Player Ataque";
			GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ().typeRobot = "Player Ataque";
			ifCondition (findR.GetComponent<Text> ());
		}

		if (num == 2) {
			findR.GetComponent<Text> ().text = "Player Defensa";
			GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ().typeRobot = "Player Defensa";
			ifCondition (findR.GetComponent<Text> ());
		}

		if (num == 3) {
			findR.GetComponent<Text> ().text = "Player Velocidad";
			GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ().typeRobot = "Player Velocidad";
			num = 0;
			ifCondition (findR.GetComponent<Text> ());
		}
	}

	///<summary>
	/////  retorna las estadisticas del tipo de esqueleto
	/// retorna  un array de float
	///</summary>
	public ArrayList SkeletonStats(float damage,float life,float speed,float cadence)
	{
		ArrayList data=new ArrayList();
		data.Add (damage);
		data.Add (life);
		data.Add (speed);
		data.Add (cadence);
		return data;
	}

	private void ifCondition(Text texto)
	{
		if(texto.text=="Player Ataque")
		{
			ArrayList stats=SkeletonStats (1, 1, 1, 1);
			GameObject.Find ("damageS").GetComponent<Text> ().text = "Daño: "+ stats [0].ToString();
			GameObject.Find("lifeS").GetComponent<Text> ().text="Vida: "+ stats [1].ToString();
			GameObject.Find("velocityS").GetComponent<Text> ().text="Velocidad: "+ stats [2].ToString();
			GameObject.Find("cadenceS").GetComponent<Text> ().text="cadencia: "+stats [3].ToString();
		}
		if(texto.text=="Player Defensa")
		{
			ArrayList stats=SkeletonStats (2, 2, 2, 2);
			//var stats = SkeletonStats (2, 12, 1, 1);
			GameObject.Find ("damageS").GetComponent<Text> ().text = "Daño: "+ stats [0].ToString();
			GameObject.Find("lifeS").GetComponent<Text> ().text="Vida: "+ stats [1].ToString();
			GameObject.Find("velocityS").GetComponent<Text> ().text="Velocidad: "+ stats [2].ToString();
			GameObject.Find("cadenceS").GetComponent<Text> ().text="cadencia: "+stats [3].ToString();
		}
		if(texto.text=="Player Velocidad")
		{
			ArrayList stats=SkeletonStats (3, 3, 3, 3);
			GameObject.Find ("damageS").GetComponent<Text> ().text = "Daño: "+ stats [0].ToString();
			GameObject.Find("lifeS").GetComponent<Text> ().text="Vida: "+ stats [1].ToString();
			GameObject.Find("velocityS").GetComponent<Text> ().text="Velocidad: "+ stats [2].ToString();
			GameObject.Find("cadenceS").GetComponent<Text> ().text="cadencia: "+stats [3].ToString();
		}
	}
}
