using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Abilities : NetworkBehaviour
{
    //tipos de robot 
    public bool typeDef = false, typeAtk = false, typeVel = false;
    private bool shieldCouldown = false;
    private bool DashCouldown = false;
    private bool rechargeCouldown = false;
    private bool jumpCouldown = false;
    private bool invisibleCouldown = false;
    private bool explosionCouldown = false;
    private bool entryInvisible = false;
    private bool explosionParticle = false;
    private GameObject smoke;
    public Transform target;
    public GameObject shield;
    public GameObject damageArea;
    public Transform spawnR;
    private float timeCoulDown = 5f;
    private float timeA = 6;
    private float timeB = 6;
    private float timeC = 6;
    private float timeD = 0;
    private float timeE = 6;
    private float timeF = 6;
    private float timeShield;
    private float timeDash;
    private float timeinvisible;
    private float timeExplosion;
    private float dash;
    public GameObject player, explosionPart;
    public Transform armT;
    // Use this for initialization

    void Awake()
    {

    }

    void Start()
    {
        CombatPieces find = GameObject.Find("CombatPieces").GetComponent<CombatPieces>();
        // Player1
        if (hasAuthority)
        {
            if (find.typeRobot == "Player Ataque")
            {
                gameObject.tag = "Player_atak";
            }
            if (find.typeRobot == "Player Defensa")
            {
                gameObject.tag = "Player_def";
            }
            if (find.typeRobot == "Player Velocidad")
            {
                gameObject.tag = "Player_vel";
            }
        }
        switch (gameObject.tag)
        {
            case "Player_atak":
                typeAtk = true;
                break;
            case "Player_def":
                typeDef = true;
                break;
            case "Player_vel":
                typeVel = true;
                break;
        }
        dash = GameManager.init.dashImpulse;
        timeShield = GameManager.init.timeShield;
        timeDash = GameManager.init.timeDash;
        timeinvisible = GameManager.init.timeinvisible;
        timeExplosion = GameManager.init.timeExplosion;
        player = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {

        if (invisibleCouldown)
        {
            if (gameObject.tag == "Player_vel")
            {
                if (entryInvisible)
                {
                    invisy();
                    //Invoke ("invisy", 0.5f);
                    entryInvisible = false;
                }
            }
        }
        active();
        CoulDown();
    }

    private void active()
    {
        Def();
        Atk();
        Vel();
    }

    private void Def()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeDef && !shieldCouldown)
        {
            shieldCouldown = true;
            //Shield (spawnR);
            if (gameObject.tag == "Player_def" && shieldCouldown)
            {
                CmdShield(spawnR.transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && typeDef && !explosionCouldown)
        {
            //habilidad daño en area
            CmdInvokeExplosion();
            GameObject clon = (GameObject)Instantiate(damageArea, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            clon.transform.localScale = new Vector3(10f, 10f, 10f);
            Destroy(clon, 1);
            Invoke("NormalVelocity", GameManager.init.slowDawntime);
        }
    }

    private void Atk()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeAtk && !jumpCouldown)
        {
            //habilidad activa saltar muros
            jumpCouldown = true;
            gameObject.GetComponent<MovimientoPersonaje>().moveDirection.y = GameManager.init.jump + GameManager.init.JumpPasiveAbilitie;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && typeAtk && !rechargeCouldown)
        {
            //habilidad activa recargar cargador
            rechargeCouldown = true;
            gameObject.GetComponent<Attack>().bullet = GameManager.init.bullet;
        }
    }

    private void Vel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeVel && !DashCouldown)
        {
            //habilidad activa Dash a donde mira
            DashCouldown = true;
            Dash();

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && typeVel && !invisibleCouldown)
        {
            invisibleCouldown = true;
            if (gameObject.tag == "Player_vel")
            {
                entryInvisible = true;
                Cmdinvisibility();
            }
        }
    }

    public void Dash()
    {
        gameObject.GetComponent<CharacterController>().Move(gameObject.GetComponent<CharacterController>().transform.forward * dash);
    }

    public void SpeedBulletBurst()
    {
        GameManager.init.frequency = 0.02f;
    }

    public void normal()
    {
        Renderer[] a = gameObject.GetComponentsInChildren<Renderer>();
        for (var y = 0; y < a.Length; y++)
        {

            for (var j = 0; j < a[y].materials.Length; j++)
            {
                a[y].materials[j].shader = Shader.Find("Standard");
            }

        }
    }

    public void CoulDown()
    {
        if (shieldCouldown)
        {
            timeA += Time.deltaTime;
            if (timeA >= timeCoulDown)
            {
                timeA = 0;
                shieldCouldown = false;

            }
        }
        //
        if (DashCouldown)
        {
            timeB += Time.deltaTime;
            if (timeB >= timeDash)
            {
                timeB = 0;
                DashCouldown = false;

            }
        }

        //
        if (invisibleCouldown)
        {
            timeD += Time.deltaTime;
            if (timeD >= timeinvisible)
            {
                timeD = 0;
                invisibleCouldown = false;

            }
        }
        //
        if (explosionCouldown)
        {
            timeE += Time.deltaTime;
            if (timeE >= timeExplosion)
            {
                timeE = 0;
                explosionCouldown = false;
            }
        }
    }


    public void NormalVelocity()
    {
        GameManager.init.speed = GameManager.init.speedNormal;
    }


    [Command]
    public void CmdShield(Vector3 arm)
    {
        armT = this.transform.Find("SpawnR").transform;
        armT.position = arm;
        GameObject clon = (GameObject)Instantiate(shield, armT.position, Quaternion.identity, armT);
        clon.transform.LookAt(target);
        Destroy(clon, timeShield);
        timeA += Time.deltaTime;
        NetworkServer.Spawn(clon);
    }

    [Command]
    private void CmdInvokeExplosion()
    {
        RpcAbilitieExplosionSpawn();
    }

    [ClientRpc]
    public void RpcAbilitieExplosionSpawn()
    {
        explosionParticle = false;
        GameObject clon = (GameObject)Instantiate(explosionPart, transform.position, Quaternion.identity, gameObject.transform);
        clon.transform.localScale = new Vector3(3f, 3f, 3f);
        NetworkServer.Spawn(clon);
        Destroy(clon, timeShield);
    }

    [Command]
    public void Cmdinvisibility()
    {
        Debug.Log("invisibilidad");
        Color color = GetComponent<Renderer>().material.color;
        Component[] z = gameObject.GetComponentsInChildren(typeof(Transform));
        foreach (Component b in z)
        {
            if (b.gameObject.GetComponent<MeshRenderer>() == null)
            {
                b.gameObject.AddComponent<MeshRenderer>();
            }
        }
        Renderer[] a = gameObject.GetComponentsInChildren<Renderer>();
        for (var y = 0; y < a.Length; y++)
        {

            for (var j = 0; j < a[y].materials.Length; j++)
            {
                a[y].materials[j].shader = Resources.Load("invisible") as Shader;
                //Resources.Load("invisible");
            }

        }
        Invoke("normal", timeinvisible);
    }

    public void invisy()
    {
        Color color = GetComponent<Renderer>().material.color;
        Component[] z = gameObject.GetComponentsInChildren(typeof(Transform));
        foreach (Component b in z)
        {
            if (b.gameObject.GetComponent<MeshRenderer>() == null)
            {
                b.gameObject.AddComponent<MeshRenderer>();
            }
        }
        Debug.Log("RevisaR");
        Renderer[] a = gameObject.GetComponentsInChildren<Renderer>();
        for (var y = 0; y < a.Length; y++)
        {

            for (var j = 0; j < a[y].materials.Length; j++)
            {
                Debug.Log(a[y].materials[j]);
                a[y].materials[j].shader = Resources.Load("invisible") as Shader;
            }

        }
        Invoke("normal", timeinvisible);
    }

}
