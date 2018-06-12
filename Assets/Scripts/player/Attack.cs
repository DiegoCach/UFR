using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Attack : NetworkBehaviour  
{
	private bool BvsBRigth=false;//if its melee
	private bool BvsBLeft=true;
	private bool shooterI=false;//if can shoot
	private bool shooterR=true;
	private bool rechargePass = false;
    CharacterController character;
    public GameObject prefab;
	public Transform spawnI;
	public Transform spawnR;
    public Transform slashPos;
    public ParticleSystem slash;
    private ParticleSystem clone;
	public int bullet;
	public int timeRechange;
	public float atackPistol=0;
	public float atackSword = 0;
    public float dmg = 1;
    public Transform arm;
    public Camera myCamera;
    private BoxCollider meleeCol; 

    private LifeContainer lifeContainer;
    public bool isAttaking = false;
	Animator anim;
    private float temps = 0;
    private bool firstCom = false;
    int noOfClicks; 
    bool canClick;

    //variablesSync
    [SyncVar]
    public float forceAtack;
    [SyncVar]
    private float timeA;
    // Use this for initialization
    void Start () 
	{
        meleeCol = gameObject.transform.GetChild(11).GetComponent<BoxCollider>();
		bullet = GameManager.init.bullet;
		anim = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        lifeContainer = GameObject.Find("vida").GetComponent<LifeContainer>();
        noOfClicks = 0;
        canClick = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (!isLocalPlayer){
			return;
		}

        if (Input.GetKeyDown(KeyCode.Mouse1)&&BvsBRigth) 																
		{
			//animation
			atackSword += 1;
			Debug.Log("golpe");
			anim.SetFloat ("atackSword", atackSword);
            clone = Instantiate(slash, slashPos);
            Destroy(clone, 0.5f);
			Invoke ("volveranim2",1.2f);

		}

        if( character.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Mouse0) && BvsBLeft)
            {
                ComboStarter();
            }
        }

        if (Input.GetButtonDown("Fire1") && shooterI && rechargePass == false)
        {
            CmdarmAssignI();
            Debug.Log("soy Cliente" + this.isClient);
            Debug.Log("soy Servidor" + this.isServer);
            CmdShooter();
            Debug.Log("disparo");
            //animation
        }

        if (Input.GetButtonDown("Fire2")&& shooterR && rechargePass==false) 																
		{
            CmdarmAssignR();
            CmdShooter();
            atackPistol += 1;
            //animation
        }
    }

    //combo

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;    
            isAttaking = true;
        }
        
        if (noOfClicks == 1)
        {
            anim.SetFloat("atackSword", 1.5f);
            CmdActivateCol();
        }
    }

    public void ComboCheck()
    {
        canClick = false;
        isAttaking = true;
        Debug.Log(noOfClicks);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ataque_espada") && noOfClicks == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            anim.SetFloat("atackSword", 0);
            canClick = true;
            isAttaking = false;
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("ataque_espada") && noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            anim.SetBool("BlendCombo2", true);
            CmdActivateCol();
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("combo2") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle         
            anim.SetBool("BlendCombo2", false);
            anim.SetFloat("atackSword", 0);
            isAttaking = false;
            canClick = true;
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("combo2") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo         
            anim.SetBool("BlendCombo3", true);
            canClick = true;
            CmdActivateCol();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("combo3"))
        { //Since this is the third and last animation, return to idle          
            anim.SetBool("BlendCombo2", false);
            anim.SetFloat("atackSword", 0);
            anim.SetBool("BlendCombo3", false);
            isAttaking = false;
            canClick = true;
            noOfClicks = 0;
        }
    }
    //deactivates collider for melee
    public void deactivateCol()
    {
        CmdDeactivateCol();
    }


    void CmdShooter()
    {
        if (timeA >= GameManager.init.frequency)
        {
            if (shooterI == false && shooterR == false)
            {
                Debug.Log("arm es un trasform null");
            }
            else
            {
                Vector3 elforward = myCamera.transform.forward * 100;
                Vector3 eldist = this.transform.position - myCamera.transform.position;
                float distSeguridad = Vector3.Dot(elforward.normalized, eldist.normalized)*eldist.magnitude;
                Debug.DrawRay(myCamera.transform.position + myCamera.transform.forward* distSeguridad, myCamera.transform.forward*100, Color.red, 2);

                RaycastHit hit;
                if (Physics.Raycast(myCamera.transform.position+myCamera.transform.forward*distSeguridad, myCamera.transform.forward, out hit, 100))
                {
                    string UIdentity = hit.collider.gameObject.name;
                    CmdWhoWasShot(UIdentity, 1);
                }
            }
        }
    }



    [Command]
    void CmdWhoWasShot(string UIdentity, float dmg)
    {
        Debug.Log(UIdentity);
        if (hasAuthority && UIdentity == "Player2")
        {
            GameManager.init.player2Hp -= dmg;
            
        }
        if (!hasAuthority && UIdentity == "Player2")
        {
            RpcTakeDamage();
        }
        if (UIdentity == "vida")
        {
            lifeContainer.hit++;
            if (lifeContainer.hit >= 3)
            {
                lifeContainer.gameObject.AddComponent<Rigidbody>();
            }
        }
    }

    //Syncronitaztion with server
    [Command]
    public void CmdarmAssignI()
    {
        arm = spawnI;
        timeA += Time.deltaTime;
    }

    [Command]
    public void CmdarmAssignR()
    {
        arm = spawnR;
        timeA += Time.deltaTime;
    }

    [ClientRpc]
    public void RpcTakeDamage()
    {
        GameManager.init.player1Hp -= dmg;
    }

    [Command]
    public void CmdActivateCol()
    {
        meleeCol.enabled = true;
        RpcActivateCol();
    }

    [Command]
    public void CmdDeactivateCol()
    {
        meleeCol.enabled = false;
        RpcDeactivateCol();
    }

    [ClientRpc]
    public void RpcActivateCol()
    {
        meleeCol.enabled = true;
    }

    [ClientRpc]
    public void RpcDeactivateCol()
    {
        meleeCol.enabled = false;
    }
}
