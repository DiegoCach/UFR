using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace ExtensionMethods
{
	public static class XMLHelper
	{
		public static void AddLegToXML(
			string path, 
			string name, 
			string sideText, 
			string elementText,
			string rarityText,
			string descriptionText, 
			float lifeText,
			float damageText,
			float speedText,
			float cadenceText)
		{
			string filepath = Application.dataPath + "/" + path;
			XmlDocument xmlDoc = new XmlDocument();

			if (File.Exists (filepath)) {
				xmlDoc.Load (filepath);

				//XmlElement elmRoot = xmlDoc.DocumentElement;

				//elmRoot.RemoveAll (); // remove all inside the transforms node.

				string id = "legs";
				string query = string.Format("//*[@id='{0}']", id);

				XmlElement legs = xmlDoc.SelectSingleNode(query) as XmlElement;

				XmlElement leg = xmlDoc.CreateElement ("Leg");
				leg.SetAttribute ("name", name);

				XmlElement side = xmlDoc.CreateElement ("Side"); 
				side.InnerText = sideText;

				XmlElement element = xmlDoc.CreateElement ("Element"); 
				element.InnerText = elementText; 

				XmlElement rarity = xmlDoc.CreateElement ("Rarity"); 
				rarity.InnerText = rarityText;

				XmlElement description = xmlDoc.CreateElement ("Description");
				description.InnerText = descriptionText;

				XmlElement life = xmlDoc.CreateElement ("Life"); 
				life.InnerText = lifeText.ToString();

				XmlElement damage = xmlDoc.CreateElement ("Damage"); 
				damage.InnerText = damageText.ToString(); 

				XmlElement speed = xmlDoc.CreateElement ("Speed");
				speed.InnerText = speedText.ToString();

				XmlElement cadence = xmlDoc.CreateElement ("Cadence");
				cadence.InnerText = cadenceText.ToString();

				legs.AppendChild (leg);
				//elmRoot.AppendChild (leg);
				leg.AppendChild (side);
				leg.AppendChild (element);
				leg.AppendChild (rarity);
				leg.AppendChild (description);
				leg.AppendChild (life);
				leg.AppendChild (damage);
				leg.AppendChild (speed);
				leg.AppendChild (cadence);

				xmlDoc.Save (filepath); // save file.
				Debug.Log ("SAVED");
			} 
			else
			{
				Debug.Log ("Esa ruta no existe:" + filepath);
			}
		}

		public static void AddArmToXML(
			string path, 
			string name, 
			string sideText, 
			string elementText,
			string rarityText,
			string descriptionText, 
			float lifeText,
			float damageText,
			float speedText,
			float cadenceText)
		{
			string filepath = Application.dataPath + "/" + path;
			XmlDocument xmlDoc = new XmlDocument();

			if (File.Exists (filepath)) {
				xmlDoc.Load (filepath);

				//XmlElement elmRoot = xmlDoc.DocumentElement;

				//elmRoot.RemoveAll (); // remove all inside the transforms node.

				string id = "arms";
				string query = string.Format("//*[@id='{0}']", id);

				XmlElement arms = xmlDoc.SelectSingleNode(query) as XmlElement;

				XmlElement arm = xmlDoc.CreateElement ("Arm");
				//arm.SetAttribute ("name", "Arm_" + sideText + "_" + elementText + "_" + rarityText + "_" + name);
				arm.SetAttribute ("name", name);

				XmlElement side = xmlDoc.CreateElement ("Side"); 
				side.InnerText = sideText;

				XmlElement element = xmlDoc.CreateElement ("Element"); 
				element.InnerText = elementText; 

				XmlElement rarity = xmlDoc.CreateElement ("Rarity"); 
				rarity.InnerText = rarityText;

				XmlElement description = xmlDoc.CreateElement ("Description");
				description.InnerText = descriptionText;

				XmlElement life = xmlDoc.CreateElement ("Life"); 
				life.InnerText = lifeText.ToString();

				XmlElement damage = xmlDoc.CreateElement ("Damage"); 
				damage.InnerText = damageText.ToString(); 

				XmlElement speed = xmlDoc.CreateElement ("Speed");
				speed.InnerText = speedText.ToString();

				XmlElement cadence = xmlDoc.CreateElement ("Cadence");
				cadence.InnerText = cadenceText.ToString();

				arms.AppendChild (arm);
				//elmRoot.AppendChild (leg);
				arm.AppendChild (side);
				arm.AppendChild (element);
				arm.AppendChild (rarity);
				arm.AppendChild (description);
				arm.AppendChild (life);
				arm.AppendChild (damage);
				arm.AppendChild (speed);
				arm.AppendChild (cadence);

				xmlDoc.Save (filepath); // save file.
				Debug.Log ("SAVED");
			} 
			else
			{
				Debug.Log ("Esa ruta no existe:" + filepath);
			}
		}

		public static void AddRobotToXML(
			string path, 
			string name,
			string baseElementText,
			string leftArmText, 
			string rightArmText,
			string legsText,
			string descriptionText, 
			float lifeText,
			float damageText,
			float speedText,
			float cadenceText)
		{
			
			string filePath;

			filePath = Application.dataPath + "/" + path;

			string piecesPath = Application.dataPath + "/Scripts/RobotsData/Robots/Robots.xml";


			XmlDocument xmlDoc = new XmlDocument();

			if (File.Exists (filePath)) {
				//xmlDoc.LoadXml (path.text);

				xmlDoc.Load (filePath);

				XmlElement elmRoot = xmlDoc.DocumentElement;
				XmlNodeList nodeList = elmRoot.SelectNodes ("MyRobots");
				nodeList = nodeList [0].ChildNodes;

				bool isHere = false;

				for (int i = 0; i < nodeList.Count; i ++ )
				{
					if(nodeList[i].Attributes["name"].Value == name){
						isHere = true;
					}
					// Now how do I get the value of each MySubElement under MyObject[i] ????
					//Debug.Log( nodeList[i].Attributes.Element["Health"].Value ); // MySubElement1
					//Debug.Log( nodeList[i].Attributes.Element["Attack"].Value ); // MySubElement2
					//Debug.Log( nodeList[i].Attributes.Element["Defense"].Value ); // MySubElement3
				}

				string idRobots = "myRobots";
				string formatRobots = "//*[@id='{0}']";
				string queryRobots = string.Format(formatRobots, idRobots);

				XmlElement robots = xmlDoc.SelectSingleNode(queryRobots) as XmlElement;
			
				if (isHere == false) {
					XmlElement robot = xmlDoc.CreateElement ("Robot");
					//arm.SetAttribute ("name", "Arm_" + sideText + "_" + elementText + "_" + rarityText + "_" + name);
					robot.SetAttribute ("name", name);

					XmlElement baseElement = xmlDoc.CreateElement ("BaseElement"); 
					baseElement.InnerText = baseElementText;

					XmlElement leftArm = xmlDoc.CreateElement ("LeftArm"); 
					leftArm.InnerText = leftArmText;

					XmlElement rightArm = xmlDoc.CreateElement ("RightArm"); 
					rightArm.InnerText = rightArmText; 

					XmlElement legs = xmlDoc.CreateElement ("Legs"); 
					legs.InnerText = legsText;

					XmlElement description = xmlDoc.CreateElement ("Description");
					description.InnerText = descriptionText;

					XmlElement life = xmlDoc.CreateElement ("Life"); 
					life.InnerText = lifeText.ToString ();

					XmlElement damage = xmlDoc.CreateElement ("Damage"); 
					damage.InnerText = damageText.ToString (); 

					XmlElement speed = xmlDoc.CreateElement ("Speed");
					speed.InnerText = speedText.ToString ();

					XmlElement cadence = xmlDoc.CreateElement ("Cadence");
					cadence.InnerText = cadenceText.ToString ();

					robots.AppendChild (robot);
					robot.AppendChild (baseElement);
					robot.AppendChild (leftArm);
					robot.AppendChild (rightArm);
					robot.AppendChild (legs);
					robot.AppendChild (description);
					robot.AppendChild (life);
					robot.AppendChild (damage);
					robot.AppendChild (speed);
					robot.AppendChild (cadence);

					xmlDoc.Save (filePath); // save file.

					Debug.Log ("Saved");
				}
				else 
				{
					Debug.Log ("Ese robot ya existe");
				}
			} 
			else
			{
				Debug.Log ("Esa ruta no existe:" + filePath);
			}
		}

	}
}
