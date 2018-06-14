using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPersonaje : NetworkBehaviour 
{
	#region FIELDS REGION
	public float speed = 1.5f;
	public float run = 0;
    public float jump = 0;
	public float gravity = 20f;
	public float moveX, moveZ, moveS, moveR;
    private bool part = false;
    public bool isJumping = false;
    public GameObject referenceFloor;
	public Rigidbody playerRb;
	public GameObject mainCamera;
    public ParticleSystem running;
	Animator anim;
    ParticleSystem clone;
    public Vector3 moveDirection = Vector3.zero;
	CharacterController character;
	public GameObject cameraPrefab;
    Attack attack;
    public bool EnteredTrigger;

    public Transform showInInspect;
    public bool slow = false;
    private bool twoKeys = false;
	#endregion

	void Start () 
	{
        if (isLocalPlayer)
        {
            mainCamera = Instantiate(cameraPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            mainCamera.name = "CameraRigPlayer1";
            CameraRig.target = transform;
            showInInspect = CameraRig.target;
            attack = GetComponent<Attack>();
            playerRb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            character = GetComponent<CharacterController>();
            jump = GameManager.init.jump;
            gravity = GameManager.init.gravity;

            speed = GameManager.init.speed;
            moveX = anim.GetFloat("BlendZ");//entra en el animator y recoge el float
            moveZ = anim.GetFloat("BlendX");

        } else
        {
            return;
        }
        
	}



    void Update () 
	{

		if (isLocalPlayer) {
			GameManager.init.speed = speed;
			StartCoroutine ("Move");
			moveX = 0.0f; 
			anim.SetFloat ("BlendX", moveX);
			moveZ = 0.0f;
			anim.SetFloat ("BlendZ", moveZ);
			Quaternion CharacterRotation = mainCamera.transform.GetChild (0).transform.GetChild (0).GetComponent<Camera> ().transform.rotation;
			CharacterRotation.x = 0;
			CharacterRotation.z = 0;

			transform.rotation = CharacterRotation;
            if (attack.isAttaking == false)
            {
                Movimiento();
            }
		} else {
			return;
		}

        if (attack.isAttaking == true)
        {
            anim.SetFloat("BlendZ", 0);
            anim.SetFloat("BlendR", 0);
            anim.SetFloat("BlendS", 0);
            anim.SetFloat("BlendX", 0);
        }
	}


									//Corrutina para el movimiento:
	//-------------------------------------------------------------------------------------------------------------------------
	/**
	 * Esta coorrutina ejecuta las animaciones 
	 **/
	IEnumerator Move() 
	{
		if (Input.GetKey (KeyCode.W) && attack.isAttaking == false ) 
		{
			for (float i = 0.0f; i <= 5.0f; i += 1f) 
			{
				moveZ = 1.5f;
				anim.SetFloat ("BlendZ", moveZ);
				yield return null;
			}
        }

		if (Input.GetKey (KeyCode.D) && attack.isAttaking == false) 
		{
			for (float i = 0.0f; i <= 5.0f; i += 1f) 
			{
                moveX = 1.5f;
				anim.SetFloat ("BlendX", moveX);

				yield return null;
			}
		}
		if (Input.GetKey (KeyCode.A) && attack.isAttaking == false ) 
		{
			for (float i = 0.0f; i >= -10.0f; i -= 1f) 
			{
                moveX = 2;
				anim.SetFloat ("BlendX", moveX);

				yield return null;
			}
		}
		if (Input.GetKey (KeyCode.S) && attack.isAttaking == false && isJumping == false) 
		{
			for (float i = 0.0f; i <= 10.2f; i += 1f) 
			{
                moveZ = 2;
				anim.SetFloat ("BlendZ", moveZ);

				yield return null;
			}
		}

        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) && attack.isAttaking == false)
        {
            for (float i = 0.0f; i >= -10.0f; i -= 1f)
            {
                moveX = 0;
                anim.SetFloat("BlendX", moveX);

                yield return null;
            }
        }
        //crouch
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && character.isGrounded && attack.isAttaking == false && isJumping == false)
        {
            for (float i = 0.0f; i <= 3.0f; i += 1f)
            {
                anim.SetBool("BlendWalkCrouch",true);
                yield return null;
            }

        }
        else if (!Input.GetKey(KeyCode.LeftControl) || !Input.GetKey(KeyCode.W) && character.isGrounded)
        {
            anim.SetBool("BlendWalkCrouch",false);
            yield return null;
        }
        if (Input.GetKey(KeyCode.LeftControl) && character.isGrounded && attack.isAttaking == false && isJumping == false)
        {
            for (float i = 0.0f; i <= 3.0f; i += 1f)
            {
                anim.SetBool("BlendIddleCrouch", true);
                yield return null;
            }

        }
        else if (!Input.GetKey(KeyCode.LeftControl) && character.isGrounded)
        {
            anim.SetBool("BlendIddleCrouch", false);
            yield return null;
        }
        //JUmp:

        if (Input.GetKeyDown(KeyCode.Space) && attack.isAttaking == false && isJumping == false)
		{
            isJumping = true;
			for (float i = 0.0f; i <= 5.0f; i += 1f) 
			{
				moveS = 1.5f;
				anim.SetFloat ("BlendS", moveS);
                if (character.isGrounded)
                {
                    anim.SetFloat("BlendS", -1);
                }
				yield return new WaitForSeconds(0.1f);
                if (!character.isGrounded)
                {
                    anim.SetFloat("BlendS", -1);
                    anim.SetBool("BlendFloating", true);
                }
                if (character.isGrounded)
                {
                    anim.SetBool("BlendFloating", false);
                    anim.SetBool("BlendFall", true);
                }
                yield return new WaitForSeconds(0.06f);
            }
            
            anim.SetBool("BlendFall",false);
			Debug.Log("salto");
            isJumping = false;
			yield return null;
		}

		//Run

		if (Input.GetKey(KeyCode.LeftShift) && character.isGrounded && attack.isAttaking == false && isJumping == false)
		{
            if (!running.isPlaying)
            {
                running.Play();
            }
            for (float i = 0.0f; i <= 3.0f; i += 1f) 
			{
				moveR = 2;
				anim.SetFloat ("BlendR", moveR);
                yield return null;
			}
			
		}else if (!Input.GetKey (KeyCode.LeftShift) && character.isGrounded) {
			anim.SetFloat ("BlendR", -2);
			moveR = 0;
            if (running.isPlaying)
            {
                running.Stop();
            }
            part = false;
            yield return null;
		}
    }


	//-------------------------------------------------------------------------------------------------------------------------

												//Function for movement:
	//-------------------------------------------------------------------------------------------------------------------------
	/**
	 *. 
	**/
	public void Movimiento()
	{
		//Can jump and move
		if (character.isGrounded && attack.isAttaking == false) 
		{
			moveDirection=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                twoKeys = false;
            } else if ((Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A)))
            {
                twoKeys = true;
            }
            moveDirection = transform.TransformDirection (moveDirection);//direccion del vector es la direccion de nuetro transform
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftControl) || slow == true) && !Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= 1.5f;
            }
            else
            {
                moveDirection *= GameManager.init.speed;
            }
		} 

        //JUmp
		if (Input.GetKeyDown(KeyCode.Space) && character.isGrounded && attack.isAttaking == false )
        {
			moveDirection.y = jump * 1.5f; // nunca puede ir delante la gravedad o no podra saltar
        }
			
		//Crouch
		if (Input.GetKeyDown(KeyCode.LeftControl) && character.isGrounded && attack.isAttaking == false && isJumping == false)
		{
			playerRb.gameObject.GetComponent<CapsuleCollider>().height=0.70f;
		}
		if (Input.GetKeyUp(KeyCode.LeftControl) && character.isGrounded && attack.isAttaking == false && isJumping == false)
		{
			playerRb.gameObject.GetComponent<CapsuleCollider>().height=0.98f;

		}


        // run
        if (Input.GetKey(KeyCode.LeftShift) && slow == false)
        {
            speed = 5;
        } else if (Input.GetKey(KeyCode.LeftShift) && slow == true)
        {
            speed = 3;
        }
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
            speed = 3;
		}

		moveDirection.y -= gravity * Time.deltaTime;//gravity for the player
		character.Move (moveDirection*Time.deltaTime);
    }

}