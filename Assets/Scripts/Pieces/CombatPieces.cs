using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class CombatPieces : NetworkBehaviour
{

    public Piece leftArm;
    public Piece rightArm;
    public Piece legs;
    public Piece head;
    public Piece chest;
    public string typeRobot;
	public int damageT;
	public int lifeT;
	public int velocityT;
	public int cadenceT;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisplayList()
    {
        if (leftArm != null && rightArm != null && legs != null && head != null && chest != null)
        {

            /*LoadData("leftArm", leftArm);
			LoadData("rightArm", rightArm);
			LoadData("legs", legs);
			LoadData("head", head);
			LoadData("chest", chest);*/

            GameObject.Find("rarity").GetComponent<Text>().text = "";
            GameObject.Find("damage").GetComponent<Text>().text = "Daño: " +
                (leftArm.damage + rightArm.damage + legs.damage + head.damage + chest.damage).ToString();
            GameObject.Find("life").GetComponent<Text>().text = "Vida: " +
                (leftArm.life + rightArm.life + legs.life + head.life + chest.life).ToString();
            GameObject.Find("velocity").GetComponent<Text>().text = "Velocidad: " +
                (leftArm.speedMovement + rightArm.speedMovement + legs.speedMovement + head.speedMovement + chest.speedMovement).ToString();
            GameObject.Find("cadence").GetComponent<Text>().text = "Cadencia: " +
                (leftArm.cadence + rightArm.cadence + legs.cadence + head.cadence + chest.cadence).ToString();
        }
    }

    public void LoadData(string bodyName, Piece data)
    {
        GameObject bodyPart = GameObject.Find(bodyName);
        //bodyPart.GetComponent<MeshFilter> ().mesh = data.skin;
        //bodyPart.GetComponent<MeshCollider> ().sharedMesh = data.skin;
    }
}