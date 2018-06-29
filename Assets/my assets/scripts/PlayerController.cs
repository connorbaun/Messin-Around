using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //PlayerController will simply COLLECT AND STORE the player's inputs. The results of those inputs will be sent over to PlayerMotor, where we will PERFORM the movements of the player.

    //here at the top, we will create some variables that we will need in this script
    public int playerNumber = 1;
    public float speed = 3; //set in inspector. the speed at which our player character will move
    public float jumpForce = 250;
    public Animator anim;
    public float hSensitivity = 3; //set in inspector. the speed at which our player character will look left and right
    public float vSensitivity = 1; //set in inspector. the speed at which our player character will look up and down
    public WeaponSwap weapon;
    public GameObject pickupPrompt;
    public GameObject reloadPrompt;
    public GameObject reticule;
    //public GameObject hitMarker;
    //public GameObject torsoMarker;
    public GameObject bulletSpawner;
    public bool drawReticule = true;





    //public float xSpeed = 3;
    //public float zSpeed = 5;



    [SerializeField]
    private int currentWeapon = 0;
    private bool isMoving;
    private int minWeapon = 0;
    private int maxWeapon = 2;
    private PlayerMotor motor; //we are creating a reference to the PlayerMotor script, which we will refer to as 'motor' inside of this script.
    private PlayerShoot shoot; //we are creating a reference to the PlayerShoot script, which we will refer to as "shoot" inside of this script.
    private PlayerGrenade grenade; //we are creating a reference to the PlayerGrenade script, which we will refer to as "grenade" inside of this script.
    private SpawnEnemy spawnEnemy; //enemy spawning script
    private PlayerController player;
    private GrenadeAmmo grenades;
    private float timer = 0;
    private float hitmarkerTime = 0.05f;
    private float headshotmarkerTime = 0.3f;




 



    // Use this for initialization
    void Start()
    {
        //setup any variables/references we will need at the start of the game
        //hitMarker.SetActive(false);
        //torsoMarker.SetActive(false);
        pickupPrompt.SetActive(false);
        reloadPrompt.SetActive(false);
        anim.SetBool("Scoped", false);
        grenades = FindObjectOfType<GrenadeAmmo>();


       player = GetComponent<PlayerController>();

        motor = GetComponent<PlayerMotor>(); //we tell Unity to grab the PlayerMotor script component attached to this object, and call it 'motor'. Now Unity knows what we mean when we say 'motor' later o
        shoot = GetComponent<PlayerShoot>(); //we tell Unity to grab the PlayerShoot script component attached to this object, and call it 'shoot.' Now Unity knows what we mean when we say 'shoot' later on
        spawnEnemy = GetComponent<SpawnEnemy>(); //ref to spawn enemy through the other script
        grenade = GetComponent<PlayerGrenade>(); //we tell Unity to grab the PlayerGrenade script component attached to this object, and call it 'grenade.' Now Unity knows what we mean when we say 'grenade' later on
    }

    // Update is called once per frame
    void Update()
    {
        //inside update, we want to constantly be checking for the player's inputs during the game, and send the results over to PlayerMotor script

        //A player's inputs can be stored inside of a container called a 'float'. a float is literally just a number that we can make up a name for

        //in this game, we will want to CONSTANTLY check whether the player is trying to move forward (walking), and also whether the player is trying to move left and right (strafing)

        //therefore, we will need one float for storing player's forward/backward input, and another float that stores strafing (left\right) input

        //moving horizontally (left/right) is referred to in Unity as moving along the "x-axis". therefore, we will call inputs trying to move left and right "xInput"

        //moving forward and backward (walking/backing up) is referred to in Unity as moving along the "z-axis". therefore, we will call inputs trying to move forward/back "zInput"

        
            float xInput = Input.GetAxisRaw("Horizontal"); //Input.GetAxisRaw is a function which tells Unity to collect the player's button/analog stick presses as either a -1, 0, or 1.

            //if the player tries to move left, and pushes left on his analog stick, Input.GetAxisRaw will be equal to -1. If he pushes nothing, it will be 0. If he pushes right, it will be 1.
            //at any given time, xInput will be equal to whatever the player is pushing, since update is called every single frame of your game. xInput is constantly updating every milisecond.

            float zInput = Input.GetAxisRaw("Vertical"); //Input.GetAxisRaw is a function which tells Unity to collect the player's button/analog stick presses as either a -l, 0, or 1.

            //if the player tries to walk forward, and pushes forward on his analog stick, Input.GetAxisRaw will be equal to 1. If he pushes nothing, it will be 0. If he pushes back, it will be -1.
            //at any given time, zInput will be equal to whatever the player is pushing, since update is called every single frame of your game. zInput is constantly updating every milisecond.

            //now that we are storing the player's x and z inputs from the left stick, we need to tell Unity how to use that data to affect the player object

            //we can use another data type called a vector3 to influence the position of an object

            //we will need one vector3 that tells Unity how to affect the player object's x position, and another vector3 to tell Unity how to affect the player object's z position

            Vector3 xDirection = transform.right * xInput;
            Vector3 zDirection = transform.forward * zInput;

            //next we will need a vector3 which combnines the player's x and z directions. to do this, we will create a new vector3 and simply add the x and z vector3's together

            Vector3 _velocity = (xDirection + zDirection).normalized * speed;


            if (_velocity != Vector3.zero)
            {
                anim.SetBool("Moving", true);
            }

            if (_velocity == Vector3.zero)
            {
                anim.SetBool("Moving", false);
            }


            //finally, for oraganization's sake, we will call the ReceiveVelocity() script on PlayerMotor and give it _velocity as a parameter.
            motor.ReceiveVelocity(_velocity);

            //now we need to do the same thing for the right analog stick. this will handle player object's rotation and the camera's rotation

            float hLook = Input.GetAxisRaw("RSHorizontal"); //first we check if the player is pushing left or right on the right stick

            Vector3 _rotation = new Vector3(0f, hLook, 0f) * hSensitivity; //next, we turn that input into a vector3 which will be used to rotate the player object

            motor.ReceiveRotation(_rotation); //like before, we send that vector3 over to PlayerMotor

            //Same thing with the vertical input of RS

            float vLook = Input.GetAxisRaw("RSVertical"); //check if player is pushing vertically on RS

            Vector3 _camRotation = new Vector3(vLook, 0f, 0f) * vSensitivity; //save that input in a vector3 and multiply it by our looksensitivity

            motor.ReceiveCamRotation(_camRotation);//send the vector3 over to PlayerMotor for moving the rigidbody

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            Debug.Log("p1 jump");
        }
        



        //CHECK FOR SHOOTING INPUT:

        

        
        
            /* if (Input.GetButton("Fire2")) //if the player HOLDS whatever button we labeled as "FIRE2" in the Input Manager...
            {
                shoot.AutoPhysicsShoot(); //call the automatic fire shoot function inside the PlayerShoot script
            } */

        


        
           /* if (Input.GetButtonDown("Fire2")) //if the player pushes whatever button we labeled as FIRE3 in the Input Manager...
            {
                StartCoroutine(shoot.BurstPhysicsShoot()); //call the burstfire shoot function inside the player shoot script. coroutine allows us to time our bullet spawns better
            } */

        if (Input.GetButtonDown("Fire3"))
        {
            if (grenades.p1Grenades > 0)
            {
                grenade.ThrowGrenade();
                anim.Play("Grenading");
                grenades.p1Grenades -= 1;
            }

        }


        if (anim.GetBool("Scoped") == true)
        {
            drawReticule = false;
        }

        else
        {
            drawReticule = true;
        }

        if (drawReticule == true)
        {
            reticule.SetActive(true);
        }else if (drawReticule == false)
        {
            reticule.SetActive(false);
        }


}






    /*public void Hitmarker()
    {
        StartCoroutine(DrawHitmarker());
    }

    public void TorsoMarker()
    {
        StartCoroutine(DrawTorsomarker());
    }

    public IEnumerator DrawHitmarker()
    {
        hitMarker.SetActive(true);
     
       yield return new WaitForSeconds(headshotmarkerTime);

        hitMarker.SetActive(false);         
    }

    public IEnumerator DrawTorsomarker()
    {
        if (torsoMarker.activeInHierarchy == true)
        {
            torsoMarker.SetActive(false);
        }

        torsoMarker.SetActive(true);

        yield return new WaitForSeconds(hitmarkerTime);

        torsoMarker.SetActive(false);
    } */

    


















    }

