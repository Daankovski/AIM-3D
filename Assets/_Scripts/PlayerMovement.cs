﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private int hasItem = 0;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private Text livesText;
    
    private Rigidbody playerRigidbody;
    private GameObject player;

    private GameObject jumpPad;
    private jumpPadScript jumpPadScript;

    // Player Related Variables
    private Vector3 movementVector;
    [SerializeField]
    private float f_movementSpeed;
    private bool isGrounded;
    [SerializeField]
    private float f_playerDamage = 0;

    // Joystick Related Variables & Related Code <--- overzetten naar nieuwe class
    [SerializeField]
    private int i_joystickNumber;
    private float f_leftStick_X;
    private float f_leftStick_Y;
    private float f_rightStick_X;
    private float f_rightStick_Y;
    private float f_rightTrigger;
    private float f_leftTrigger;
    

    void Start () {
        
        jumpPad = GameObject.Find(Tags.jumpPad);
        jumpPadScript = jumpPad.GetComponent<jumpPadScript>();

        playerRigidbody = GetComponent<Rigidbody>();
        player = this.gameObject;

        ControllerToPlayer();
    }
	
	
	void Update () {
        PlayerHealthSystem();
        PlayerMovementManager();
        if (Input.GetKeyDown(KeyCode.Space) && hasItem != 0)
        {
            Debug.Log("shoot!");
            hasItem = 0;
        }
    }

    void FixedUpdate() {
        ControllerConfig();    

    }

    // Getters & Setters
    public float playerDamage {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
    }
    public int HasItem
    {
        get { return hasItem; }
        set { hasItem = value; }
    }

    void ControllerConfig() {
        // Onderstaande tekst in void ControllerConfig
        string joystickString = i_joystickNumber.ToString();
        f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString);
        f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString);

        f_rightStick_X = Input.GetAxis("RightJoystickX_P" + joystickString);
        f_rightStick_Y = Input.GetAxis("RightJoystickY_P" + joystickString);

        f_rightTrigger = Input.GetAxis("RightTrigger_P" + joystickString);
        f_leftTrigger = Input.GetAxis("LeftTrigger_P" + joystickString);
    }

    
    void ControllerToPlayer() { // Makes it so that the playerTag assigned to an object equals the joystickNumber of the Controller
        switch (player.gameObject.tag) {
            case Tags.player1:
                i_joystickNumber = 1;
                break;
            case Tags.player2:
                i_joystickNumber = 2;
                break;
            case Tags.player3:
                i_joystickNumber = 3;
                break;
            case Tags.player4:
                i_joystickNumber = 4;
                break;
            default:
                i_joystickNumber = 0;
                break;

                
        }
    
        
    }
    
    void PlayerMovementManager() {
        
        // Movement
        movementVector.y = 0f;
        movementVector = new Vector3(f_leftStick_X, movementVector.y * jumpPadScript.launchPower, -f_leftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed , ForceMode.VelocityChange);
       
        // Rotation     
        if (movementVector.sqrMagnitude < 0.1f)
        {
            return;
        }
        var lookAngleL = Mathf.Atan2(f_leftStick_X, f_leftStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngleL, 0);
        


        if (f_rightTrigger > 0)
        {
            f_movementSpeed = .5f;
        }
        else if (f_leftTrigger > 0)
        {
            f_movementSpeed = f_movementSpeed + 1f;
        }
        else {
            f_movementSpeed = 0f;
        }

        


    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            float totalVeclocity = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z);
            Vector3 velocityOther = col.gameObject.GetComponent<Rigidbody>().velocity;
            float totalVeclocityOther = Mathf.Abs(velocityOther.x) + Mathf.Abs(velocityOther.z);
            if(totalVeclocity < totalVeclocityOther)
            {
                f_playerDamage += totalVeclocityOther - totalVeclocity;
                Debug.Log(f_playerDamage);
            }
            velocity *= 1f + f_playerDamage/100f;
        }
    }
    void OnCollisionStay(Collision col) {
        if (col.gameObject.tag == "slippery")
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }

    void PlayerHealthSystem() {
        if (f_playerDamage == 100) {

            f_playerDamage = 0;
            StartCoroutine(Die());
            
        }
    }
    IEnumerator Die()
    {
        lives--;
        livesText.text = "lives: " + lives;

        //creates an explosion if the player dies.
        explosion.transform.position = transform.position;
        Instantiate(explosion);

        //for the duration the player is not visible while it still exists.
        transform.position = new Vector3(0f, 100f, 0f);

        //waits for 2 seconds.
        yield return new WaitForSeconds(2f);

        //checks if the player has still lives left to respawn.
        if(lives ==0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(0f,5f,0f);
        }
    }

}
