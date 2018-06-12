using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Abilities : NetworkBehaviour
{
    //tipos de robot 
    public bool typeDef = false, typeAtk = false, typeVel = false;
    private bool shieldCooldown = false;
    private bool DashCooldown = false;
    private bool powerCooldown = false;
    private bool ballsCooldown = false;
    private bool invisibleCooldown = false;
    private bool explosionCooldown = false;
    private bool entryInvisible = false;
    private bool explosionParticle = false;
    private GameObject smoke;
    public Transform target;
    public GameObject shield;
    public GameObject damageArea;
    public Transform spawnR;
    private float timeCooldown = 25f;
    private float timeA, timeB, timeC, timeD, timeE, timeF, timeG;
    private float timeShield, timeDash, timeinvisible, timeExplosion;
    private float dash;
    public GameObject player, explosionPart, auraPart;
    public Transform armT, ground;
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

        if (invisibleCooldown)
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
        Cooldown();
    }

    private void active()
    {
        Def();
        Atk();
        Vel();
    }

    private void Def()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeDef && !shieldCooldown)
        {
            shieldCooldown = true;
            //Shield (spawnR);
            if (gameObject.tag == "Player_def" && shieldCooldown)
            {
                CmdShield(spawnR.transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && typeDef && !explosionCooldown)
        {
            //habilidad daño en area
            explosionCooldown = true;
            CmdInvokeExplosion();
            GameObject clon = (GameObject)Instantiate(damageArea, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            clon.transform.localScale = new Vector3(10f, 10f, 10f);
            Destroy(clon, 1);
            Invoke("NormalVelocity", GameManager.init.slowDawntime);
        }
    }

    private void Atk()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeAtk && !ballsCooldown)
        {
            ballsCooldown = true;
            gameObject.transform.GetChild(12).gameObject.SetActive(true);
            Invoke("DeactivateBalls", 8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && typeAtk && !powerCooldown)
        {
            powerCooldown = true;
            gameObject.GetComponent<Attack>().dmg = 3;
            GameObject clon = (GameObject)Instantiate(auraPart, ground.position, Quaternion.identity, gameObject.transform);
            Destroy(clon, 6);
            Invoke("DeactivatePower", 6);
        }
    }

    private void Vel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && typeVel && !DashCooldown)
        {
            //habilidad activa Dash a donde mira
            DashCooldown = true;
            Dash();

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && typeVel && !invisibleCooldown)
        {
            invisibleCooldown = true;
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

    public void Cooldown()
    {
        if (shieldCooldown)
        {
            timeA += Time.deltaTime;
            if (timeA >= timeCooldown)
            {
                timeA = 0;
                shieldCooldown = false;

            }
        }
        //
        if (DashCooldown)
        {
            timeB += Time.deltaTime;
            if (timeB >= timeCooldown)
            {
                timeB = 0;
                DashCooldown = false;

            }
        }

        //
        if (invisibleCooldown)
        {
            timeD += Time.deltaTime;
            if (timeD >= timeCooldown)
            {
                timeD = 0;
                invisibleCooldown = false;

            }
        }
        //
        if (explosionCooldown)
        {
            timeE += Time.deltaTime;
            if (timeE >= timeCooldown)
            {
                timeE = 0;
                explosionCooldown = false;
            }
        }

        if (ballsCooldown)
        {
            timeF += Time.deltaTime;
            if (timeF >= timeCooldown)
            {
                timeF = 0;
                ballsCooldown = false;
            }
        }

        if (powerCooldown)
        {
            timeG += Time.deltaTime;
            if (timeG >= timeCooldown)
            {
                timeG = 0;
                powerCooldown = false;
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

    private void DeactivateBalls()
    {
        gameObject.transform.GetChild(12).gameObject.SetActive(false);
    }

    private void DeactivatePower()
    {
        gameObject.GetComponent<Attack>().dmg = 1;
    }
}
