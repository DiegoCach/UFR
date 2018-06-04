#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

public class ScriptableObjectGenerator: MonoBehaviour {

	static String fileToLoad = "robotsData" + ".csv";

	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll();
		print ("Player preferences cleared");
	}

	[MenuItem("Tools/Import pieces data")]
	private static void ImportPiecesData (){
		string filePath = Application.dataPath + "/robotsdb/" + fileToLoad;

		if (!File.Exists (filePath)) {
			Debug.LogError ("Missing Data file: " + filePath);
			return;
		}

		string[] readText = File.ReadAllLines("Assets/robotsdb/" + fileToLoad);
		for (int i = 13; i < readText.Length; i++){
			
			Piece dataModel = ScriptableObject.CreateInstance<Piece>();
			dataModel.Load(readText[i]);

			if(dataModel.name != "#" || dataModel.name != "" || dataModel.name != " "){

				string prefix = dataModel.name.Split('_')[0];

				if (prefix == "la") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/leftArms/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				} 
				if (prefix == "ra") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/rightArms/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				}
				if (prefix == "ls") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/legs/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				} 
				if (prefix == "b") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/others/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				}
				if (prefix == "h") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/heads/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				} 
				if (prefix == "c") {
					filePath = "";
					string myfilePath = "Assets/Resources/data/chests/";
					string myfileName = string.Format ("{0}{1}.asset", myfilePath, dataModel.name);
					if (myfileName != (myfilePath + ".asset") && !File.Exists (myfilePath + dataModel.name + ".asset")) {
						//Debug.Log ("Imported: " + readText[i]);
						AssetDatabase.CreateAsset (dataModel, myfileName);
					}
				}
			}

		}
		print ("All assets has been imported");
	}
}
#endif