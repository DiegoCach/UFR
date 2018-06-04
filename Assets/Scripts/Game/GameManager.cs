using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public static GameManager init;
    public GameObject winner;
    public GameObject loser;
    public GameObject finallyG;
    public GameObject player1;
    public GameObject player2;
    public Piece pieceData;
    public bool exit = true;
    public float jump = 0;
    public float gravity = 20f;
    public float speed;
    public float speedNormal;
    public float player3;
    public float frequency = 0.05f;// velocidad de disparo
    public float slowDawnVelocity;//relentizar al recibir el daño en area
    public float slowDawntime;//tiempo que dura la disminucion de velocidad del daño en area
    public int bullet;
    public int PasiveDamageBullet;//daño de la ultima bala de la habilidad pasiva
    public float dashImpulse;//inpulso que realiza la habilidad de Dash
    public float JumpPasiveAbilitie;
    public float timeShield;
    public float timeDash;
    public float timeJump;
    public float timeinvisible;
    public float timeRecharge;
    public float timeExplosion;
    private Camera mainCamera;
    public GameObject cam;
    public GameObject spawnCam;
    public Image crossfire;
    private bool wearRobot;
    private bool wearing;
    public bool findP2;
    public bool findP1;
    bool host = true;
    public GameObject bar;
    public GameObject barp2;
    public Image[] roundsWon;
    public GameObject menuExit;
    //public NetworkClient client;
    //variablesSync
    [SyncVar]
    public float player1Hp;
    [SyncVar]
    public float player2Hp;
    [SyncVar]
    public float copyHP1;
    [SyncVar]
    public float copyHP2;
    [SyncVar]
    public float damageBullet, damageArea;
    [SyncVar]
    public bool pl1, pl2;
    [SyncVar]
    public int rounds;
    [SyncVar]
    public int roundP1 = -1;
    [SyncVar]
    public int roundP2 = 2;
    [SyncVar]
    public float timeGame, timeContinue;
    [SyncVar]
    public bool winRound;
    //player1
    [SyncVar]
    public string head, chest, leg,leftArm,rigthArm;
    //player2
    [SyncVar]
    public string head2, chest2, leg2,leftArm2,rigthArm2;


    void Awake()
    {
        init = this;
        winRound = false;
        wearing = true;
        findP1 = false;
        NetworkServer.RegisterHandler(CustomMsgID.Something, ReceiveSomethingOnServer);
    }

    // Use this for initialization
    void Start()
    {
        speed = 3f;
        mainCamera = Camera.main;
        speedNormal = speed;
        copyHP1 = player1Hp;
        copyHP2 = player2Hp;
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        findP2 = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (bar == null || barp2 == null)
        {
            bar = GameObject.Find("bar");
            barp2 = GameObject.Find("barP2");
        }
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        timeContinue += Time.deltaTime;

        if (findP2 && player1 != null)
        {
            //mirarar entrada host para que pinte ,cliente mal
            savePlayer();
            wearRobotStart();
            findP2 = false;
            findP1 = true;
        }
        if (findP1 && GameObject.Find("Player2") && hasAuthority)
        {
            if (head2 != "")
            {
                findP1 = false;
                wearRobotStart();
            }
        }
        RpcChangeLife();
    }


    public void viewGameWinLoser()
    {
        if (pl1)
        {
            if (rounds < 3)
            {
                roundP1++;
                // cartel perder
                CmdwinPlayer1();
                RpcwinPlayer2();
                player1Hp = 2;
                player2Hp = 1;
            }
            else
            {
                CmdwinPlayer1();
            }
            rounds++;

        }
        if (pl2)
        {
            if (rounds < 3)
            {
                roundP2++;
                //cartel perder
                CmdwinPlayer1();
                RpcwinPlayer2();
                player1Hp = 1;
                player2Hp = 2;
            }
            else
            {
                RpcwinPlayer2();
            }
            rounds++;

        }
    }

    [ClientRpc]
    public void RpcwinPlayer2()
    {
        // cartel ganar
        winner.SetActive(true);
        player1.GetComponent<MovimientoPersonaje>().enabled = false;
        player1.GetComponent<Attack>().enabled = false;
        player1.GetComponent<Abilities>().enabled = false;
        pl2 = false;
    }

    [Command]
    public void CmdwinPlayer1()
    {
        if (pl2)
        {
            roundsWon[roundP2].color = Color.green;
        }
        else if (pl1)
        {
            roundsWon[roundP1].color = Color.green;
        }
        Rpcgreen();
        player1.GetComponent<MovimientoPersonaje>().enabled = false;
        player1.GetComponent<Attack>().enabled = false;
        player1.GetComponent<Abilities>().enabled = false;
        pl1 = false;
    }

    [ClientRpc]
    public void Rpcgreen()
    {
        if (pl2)
        {
            roundsWon[roundP2].color = Color.green;
        }
        if (pl1)
        {
            roundsWon[roundP1].color = Color.green;
        }
    }
    public void restart()
    {
        player1.GetComponent<MovimientoPersonaje>().enabled = true;
        player1.GetComponent<Attack>().enabled = true;
        player1.GetComponent<Abilities>().enabled = true;
        player2.GetComponent<MovimientoPersonaje>().enabled = true;
        player2.GetComponent<Attack>().enabled = true;
        player2.GetComponent<Abilities>().enabled = true;
        pl1 = false;
        pl2 = false;
        winner.SetActive(false);
        player1Hp = 100;
        player2Hp = 100;
    }
    ///<summary>
    ///Busca las tres partes para vestir al robot (cabeza, cuerpo,piernas,brazos)
    ///</summary>
    public void wearRobotStart()
    {
        //wear no deberia ser si eres el cliente
        if (host)
        {
            CombatPieces combatPieces = Wear();
            for (int i = 0; i < 2; i++)
            {
                if (GameObject.Find("head"))
                {
                    pieceData = combatPieces.head;
                    findPartBody(pieceData);
                    GameObject.Find("head").name = "Head";
                }
                if (GameObject.Find("chest"))
                {
                    pieceData = combatPieces.chest;
                    findPartBody(pieceData);
                    GameObject.Find("chest").name = "Chest";
                    GameObject.Find("shoulderde").name = "Chest2";
                    GameObject.Find("shoulderiz").name = "Chest3";
                    GameObject.Find("stomach").name = "Chest4";
                }
                if (GameObject.Find("hip"))
                {
                    pieceData = combatPieces.legs;
                    findPartBody(pieceData);
                    GameObject.Find("hip").name = "Hip1";
                    GameObject.Find("femurde").name = "Hip2";
                    GameObject.Find("femuriz").name = "Hip3";
                    GameObject.Find("kneede").name = "Hip4";
                    GameObject.Find("kneeiz").name = "Hip4";
                    GameObject.Find("legde").name = "Hip5";
                    GameObject.Find("legiz").name = "Hip6";
                    GameObject.Find("footde").name = "Hip7";
                    GameObject.Find("footiz").name = "Hip8";
                    GameObject.Find("antde").name = "Hip9";
                    GameObject.Find("antiz").name = "Hip10";
                }
                //WearOnline para las piezas del otro player en la siguiente itineracion
                if (!hasAuthority)
                {
                    combatPieces = WearOnline();
                }
            }
            host = false;
        }
        if (!host && player2 != null)
        {
            CombatPieces combatPieces = WearOnline();
            if (GameObject.Find("head"))
            {
                pieceData = combatPieces.head;
                findPartBody(pieceData);
                GameObject.Find("head").name = "Head2";
            }
            if (GameObject.Find("chest"))
            {
                pieceData = combatPieces.chest;
                findPartBody(pieceData);
                GameObject.Find("chest").name = "Chest1";
                GameObject.Find("shoulderde").name = "Chest2";
                GameObject.Find("shoulderiz").name = "Chest3";
                GameObject.Find("stomach").name = "Chest4";

            }
            if (GameObject.Find("hip"))
            {
                pieceData = combatPieces.legs;
                findPartBody(pieceData);
                GameObject.Find("hip").name = "Hip";
                GameObject.Find("femurde").name = "Hip2";
                GameObject.Find("femuriz").name = "Hip3";
                GameObject.Find("kneede").name = "Hip4";
                GameObject.Find("kneeiz").name = "Hip4";
                GameObject.Find("legde").name = "Hip5";
                GameObject.Find("legiz").name = "Hip6";
                GameObject.Find("footde").name = "Hip7";
                GameObject.Find("footiz").name = "Hip8";
                GameObject.Find("antde").name = "Hip9";
                GameObject.Find("antiz").name = "Hip10";
            }
			/*if (GameObject.Find ("leftArm")) 
			{
				pieceData = combatPieces.legs;
				findPartBody(pieceData);
			}
			if (GameObject.Find ("rigthArm")) 
			{
				pieceData = combatPieces.legs;
				findPartBody(pieceData);
			}*/
        }
    }

    ///<summary>
    ///instancia cada parte de la armadura en la zona selecionada
    ///</summary>
    public void findPartBody(Piece pieceData)
    {
        if (pieceData != null)
        {
            for (int i = 0; i < pieceData.skins.Length; i++)
            {
                GameObject bodyPart = GameObject.Find(pieceData.skins[i].name);
                if (bodyPart.transform.childCount > 0)
                {
                    foreach (Transform child in bodyPart.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                GameObject part = Instantiate(pieceData.skins[i], bodyPart.transform.position, bodyPart.transform.rotation);
                part.transform.parent = bodyPart.transform;
                part.transform.localScale = pieceData.skins[i].transform.lossyScale;

            }
        }
        else
        {
            Debug.Log("no tiene armadura");
        }
    }

    ///<summary>
    ///regresa un CombatPieces con las piezas del jugador sincronizadas correctamente  
    ///</summary>
    public CombatPieces WearOnline()
    {
        //"Player2"
        Piece pieceRobot = Resources.Load("data/heads/" + head2, typeof(Piece)) as Piece;
        CombatPieces bodyCombatPieces = GameObject.Find("CombatPieces").GetComponent<CombatPieces>();
        //asignacion
        bodyCombatPieces.head = pieceRobot;
        pieceRobot = Resources.Load("data/chests/" + chest2, typeof(Piece)) as Piece;
        bodyCombatPieces.chest = pieceRobot;
        pieceRobot = Resources.Load("data/legs/" + leg2, typeof(Piece)) as Piece;
        bodyCombatPieces.legs = pieceRobot;
		//bodyCombatPieces.leftArm=
		//bodyCombatPieces.rightArm=
        return GameObject.Find("CombatPieces").GetComponent<CombatPieces>();
    }
    ///<summary>
    ///regresa un CombatPieces con las piezas del jugardor1
    ///</summary>
    public CombatPieces Wear()
    {
        //"Player1"
        Piece pieceRobot = Resources.Load("data/heads/" + head, typeof(Piece)) as Piece;
        CombatPieces bodyCombatPieces = GameObject.Find("CombatPieces").GetComponent<CombatPieces>();
        //asignacion
        bodyCombatPieces.head = pieceRobot;
        pieceRobot = Resources.Load("data/chests/" + chest, typeof(Piece)) as Piece;
        bodyCombatPieces.chest = pieceRobot;
        pieceRobot = Resources.Load("data/legs/" + leg, typeof(Piece)) as Piece;
        bodyCombatPieces.legs = pieceRobot;
		//bodyCombatPieces.leftArm=
		//bodyCombatPieces.rightArm=
        return GameObject.Find("CombatPieces").GetComponent<CombatPieces>();

    }

    ///<summary>
    ///guarda las piezas del player 1 siempre en las variables sincrenizadas
    ///</summary>
    public void savePlayer()
    {
        // Player1
        if (hasAuthority)
        {
			CombatPieces find= GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ();
			if (find.head != null &&
				find.chest != null &&
				find.legs != null)
            {
                head = find.head.name;
				chest = find.chest.name;
				leg = find.legs.name;
				//leftArm = find.leftArm.name;
				//rigthArm=find.rightArm.name;
            }
        }
        if (!hasAuthority)
        {
			CombatPieces find= GameObject.Find ("CombatPieces").GetComponent<CombatPieces> ();
			head2 = find.head.name;
			chest2 = find.chest.name;
			leg2 = find.legs.name;
			//leftArm2 = find.leftArm.name;
			//rigthArm2=find.rightArm.name;
			SendSomethingToServer(head2, chest2, leg2,leftArm2,rigthArm2);
            //NetworkServer.UnregisterHandler (CustomMsgID.Something);
            //CmdEnviar(head2);
        }
    }

    //Parte para los mensajes de red


    ///<summary>
    ///// Sus ID de mensajes personalizados 
    /// Estos valores deben ser únicos ,para estar seguros, comenzamos los personalizados en 1000.
    ///</summary>
    /// 
    public class CustomMsgID
    {
        public static short Something = 1001;
    };

    ///<summary>
    /////  enviar datos atraves de la red, constructor del mensaje
    /// Mensaje que se enviara con la cabeza,cuerpo y piernas
    ///</summary>
    public class SomethingMessage : MessageBase
    {
        public string headE, chestE, legE,rigthArmE,leftArmE;
    }

    ///<summary>
    /////  El cliente llamaría a esta función con el valor que quiera enviar.
    /// datos de los mensajes
    ///</summary>
	public void SendSomethingToServer(string headR, string chestR, string legR, string leftArmR, string rigthArmR)
    {
        var msg = new SomethingMessage();

        msg.headE = headR;
        msg.chestE = chestR;
        msg.legE = legR;
		msg.leftArmE = leftArmR;
		msg.rigthArmE = rigthArmR;
        GameObject.Find("NetworkController").GetComponent<NetworkController>().client.Send(CustomMsgID.Something, msg);

    }
    ///<summary>
    ///// El servidor recibe el mensaje del cliente con el valor que enviaron.
    /// Recibidor asigna al gameManager el mensaje recibido del cliente
    ///</summary>
    public void ReceiveSomethingOnServer(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<SomethingMessage>();
        GameManager.init.head2 = msg.headE;
        GameManager.init.chest2 = msg.chestE;
        GameManager.init.leg2 = msg.legE;
		GameManager.init.leftArm2 = msg.leftArmE;
		GameManager.init.leftArm2 = msg.rigthArmE;
    }

    [ClientRpc]
    void RpcChangeLife()
    {
        bar.GetComponent<Image>().fillAmount = player1Hp / 100;
        barp2.GetComponent<Image>().fillAmount = player2Hp / 100;
    }

    public void exitMenu()
    {
        if (exit && player1 != null && Input.GetKeyDown(KeyCode.Escape))
        {
            menuExit.SetActive(true);
            exit = false;
        }
    }
}
