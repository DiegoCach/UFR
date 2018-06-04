using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class UpdateMyRobot : MonoBehaviour {

	protected DataModel oldRobot, newRobot, robotBase, leftArm, rightArm, legs;
	protected string robotBaseName, leftArmName, rightArmName, legsName;
	protected string leftArmType, rightArmType, legsType;
	protected string robot;

	void Awake()
	{
		robot = PlayerPrefs.GetString ("robotInicial", "myRobot");

		if(robot == ""){
			PlayerPrefs.SetString ("robotBaseName", robot + "Base");
		}

		robotBaseName = PlayerPrefs.GetString ("robotBaseName");
		leftArmName = PlayerPrefs.GetString ("leftArmName");
		rightArmName = PlayerPrefs.GetString ("rightArmName"); 
		legsName = PlayerPrefs.GetString ("legsName");

		if(leftArmName == "" || rightArmName == "" || legsName == "" )
		{
			leftArmType = robot;
			rightArmType = robot;
			legsType = robot;
			PlayerPrefs.SetString ("leftArmType", leftArmType);
			PlayerPrefs.SetString ("rightArmType", rightArmType);
			PlayerPrefs.SetString ("legsType", legsType);
		}
	}
		
	void Start ()
	{
		UpdateRobot();
	}
		
	protected void SetRobotInfo()
	{
		leftArmType = PlayerPrefs.GetString ("leftArmType");
		rightArmType = PlayerPrefs.GetString ("rightArmType"); 
		legsType = PlayerPrefs.GetString ("legsType");

		PlayerPrefs.SetString ("robotBaseName", robot + "Base");
		PlayerPrefs.SetString ("leftArmName", "leftArm" + leftArmType + "In" + robot);
		PlayerPrefs.SetString ("rightArmName", "rightArm" + rightArmType + "In" + robot);
		PlayerPrefs.SetString ("legsName", "legs" + legsType + "In" + robot);
	}

	protected void GetRobotInfo()
	{
		robotBaseName = PlayerPrefs.GetString ("robotBaseName");
		leftArmName = PlayerPrefs.GetString ("leftArmName");
		rightArmName = PlayerPrefs.GetString ("rightArmName"); 
		legsName = PlayerPrefs.GetString ("legsName");

		robotBase = Load (robotBaseName);
		leftArm = Load (leftArmName);
		rightArm = Load (rightArmName);
		legs = Load (legsName);
	}

	protected DataModel UpdatedAtributesOf(DataModel oldRobot)
	{
		oldRobot = Load ("myRobot");
		oldRobot.damage = robotBase.damage + leftArm.damage + rightArm.damage + legs.damage;
		oldRobot.life = robotBase.life + leftArm.life + rightArm.life + legs.life;
		oldRobot.speedMovement = robotBase.speedMovement + leftArm.speedMovement + rightArm.speedMovement + legs.speedMovement;
		oldRobot.cadence = robotBase.cadence + leftArm.cadence + rightArm.cadence + legs.cadence;
		return oldRobot;
	}

	protected void SaveInfo(DataModel dataModel)
	{
		dataModel = ScriptableObject.CreateInstance<DataModel>();
		//ScriptableObject.
		#if UNITY_EDITOR
		EditorUtility.SetDirty (dataModel);

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();
		#endif
	}

	public void UpdateRobot()
	{
		SetRobotInfo ();
		GetRobotInfo ();
		newRobot = UpdatedAtributesOf (oldRobot);
		SaveInfo (newRobot);
		Debug.Log(newRobot.damage);
		Debug.Log(newRobot.life);
		Debug.Log(newRobot.speedMovement);
		Debug.Log(newRobot.cadence);
		Debug.Log("--------------------");
	}

	void OnGUI()
	{
		//Delete all of the PlayerPrefs settings by pressing this Button
		if (GUI.Button(new Rect(100, 200, 70, 30), "Delete"))
		{
			PlayerPrefs.DeleteAll();
			Debug.Log ("Deleted");
		}

		if (GUI.Button(new Rect(30, 10, 130, 30), "leftArmDefense"))
		{
			PlayerPrefs.SetString ("leftArmType", "Defense");
		}

		if (GUI.Button(new Rect(30, 50, 130, 30), "rightArmStrength"))
		{
			PlayerPrefs.SetString ("rightArmType", "Strength");
		}

		if (GUI.Button(new Rect(30, 90, 130, 30), "legsStrength"))
		{
			PlayerPrefs.SetString ("legsType", "Strength");
		}

		if (GUI.Button(new Rect(200, 90, 130, 30), "Confirmar"))
		{
			UpdateRobot();
		}
	}

	protected DataModel Load(string name){return Resources.Load("dataObjects/" + name) as DataModel;}

	protected void UnloadAsset(DataModel datamodel){Resources.UnloadAsset (datamodel);}
}
