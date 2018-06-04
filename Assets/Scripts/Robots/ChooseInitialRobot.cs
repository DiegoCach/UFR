using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseInitialRobot : MonoBehaviour {
	
	protected Text descriptionText, nameText, damageText, lifeText, speedText, cadenceText;
	protected int num = 0;
	protected string[] modelsName = {"Strength", "Agility", "Defense"};
	public GameObject[] robots;

	// Use this for initialization
	void Start () {
		robots = new GameObject[modelsName.Length];

		descriptionText = GameObject.Find ("Description").GetComponent<Text> ();
		nameText = GameObject.Find ("Type").GetComponent<Text> ();
		damageText = GameObject.Find ("daño").GetComponent<Text> ();
		lifeText = GameObject.Find ("vida").GetComponent<Text> ();
		speedText = GameObject.Find ("velocidad").GetComponent<Text> ();
		cadenceText = GameObject.Find ("cadencia").GetComponent<Text> ();

		for (int i = 0; i < modelsName.Length; i++){
			robots [i] = GameObject.Find (modelsName[i]);
		}

		num = 0;
		GetText (Load (modelsName[num]));

	}

	public void LoadNextRobot(){
		if(num < modelsName.Length-1){
			num = num + 1;
			GetText (Load (modelsName[num]));

			robots [num-1].transform.position = new Vector3 (-2.5f,1.3f,1.4f);
			robots [num].transform.position = new Vector3 (0,1,0);
			if (num == modelsName.Length - 1) {
				robots [0].transform.position = new Vector3 (2.5f, 1.3f, 1.4f);
			} else {
				robots [num + 1].transform.position = new Vector3 (2.5f, 1.3f, 1.4f);
			}

		} else if(num == modelsName.Length-1){
			num = 0;
			GetText (Load (modelsName[num]));

			robots [modelsName.Length - 1].transform.position = new Vector3 (-2.5f,1.3f,1.4f);
			robots [num].transform.position = new Vector3 (0,1,0);
			robots [num + 1].transform.position = new Vector3 (2.5f, 1.3f, 1.4f);
		}
	}

	public void nextScene(){
		PlayerPrefs.SetString ("robotInicial", modelsName[num]);
		SceneManager.LoadScene("cambiarPiezas");
	}

	public void LoadPreviousRobot(){
		if(num <= modelsName.Length-1 && num != 0 ){
			num = num - 1;
			GetText (Load (modelsName[num]));

			if (num == 0) {
				robots [modelsName.Length-1].transform.position = new Vector3 (-2.5f,1.3f,1.4f);
			} else {
				robots [num-1].transform.position = new Vector3 (-2.5f,1.3f,1.4f);
			}
			robots [num].transform.position = new Vector3 (0,1,0);
			robots [num + 1].transform.position = new Vector3 (2.5f, 1.3f, 1.4f);

		} else if(num == 0){
			num = modelsName.Length - 1;
			GetText (Load (modelsName[num]));

			robots [num - 1].transform.position = new Vector3 (-2.5f,1.3f,1.4f);
			robots [num].transform.position = new Vector3 (0,1,0);
			robots [0].transform.position = new Vector3 (2.5f, 1.3f, 1.4f);
		}
	}
	void GetText (DataModel robot){
		nameText.text = robot.name;
		damageText.text = "Daño: " + robot.damage.ToString();
		lifeText.text = "Vida: " + robot.life.ToString();
		speedText.text = "Velocidad: " + robot.speedMovement.ToString();
		cadenceText.text = "Cadencia: " + robot.cadence.ToString();
		descriptionText.text = robot.description;
	}

	protected DataModel Load(string name){return Resources.Load("dataObjects/" + name) as DataModel;}

	protected void UnloadAsset(DataModel datamodel){Resources.UnloadAsset (datamodel);}
}
