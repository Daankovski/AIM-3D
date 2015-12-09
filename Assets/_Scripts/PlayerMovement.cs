using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private int hasItem = 0;

    private Rigidbody playerRigidbody;
    private GameObject player;

    // Player Related Variables
    private float f_movementSpeed = 20f;
    private bool isGrounded;
    [SerializeField]
    private float f_playerDamage = 0;
    private float f_addedVelocity = 6f;

    // Joystick Related Variables
    [SerializeField]
    private int i_joystickNumber;
    private string joystickString;
    private float f_leftStick_X;
    private float f_leftStick_Y;

    private float xSpeed = 1f;
    private float ySpeed = 0f;
    [SerializeField]
    private float maxSpeed = 15f;
    [SerializeField]
    private float aceleration = 1f;
    [SerializeField]
    private float friction = 0.1f;


    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerObj");

        ControllerToPlayer();
    }
	
	
	void Update () {
        f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString);
        f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString);
        //Debug.Log(f_leftStick_X);
        if (Input.GetKeyDown(KeyCode.Space) && hasItem != 0)
        {
            Debug.Log("shoot!");
            hasItem = 0;
        }
    }

    void FixedUpdate() {
        //Debug.Log(f_playerDamage);
        //Debug.Log(GetComponent<Collider>().material.dynamicFriction);
        PlayerMovementManager();
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

    void ControllerToPlayer() { // Makes it so that the playerTag assigned to an object equals the joystickNumber of the Controller
        switch (player.gameObject.tag) {
            case "Player1":
                i_joystickNumber = 1;
                break;
            case "Player2":
                i_joystickNumber = 2;
                break;
            case "Player3":
                i_joystickNumber = 3;
                break;
            case "Player4":
                i_joystickNumber = 4;
                break;
            default:
                i_joystickNumber = 0;
                break;
        }

        joystickString = i_joystickNumber.ToString();
    }

    void PlayerMovementManager() {
        // Horizontal & Vertical Movement

        //oude code:
        //Vector3 movementVector = new Vector3(f_leftStick_X * f_movementSpeed, 0f, f_leftStick_Y * -f_movementSpeed);

        if (f_leftStick_X > 0 && xSpeed < maxSpeed)
        {
            xSpeed += aceleration *f_leftStick_X;
        }
        else if (f_leftStick_X < 0 && xSpeed > -maxSpeed)
        {
            xSpeed -= aceleration *-f_leftStick_X;
        }
        else
        {
            if (xSpeed < 0)
            {
                xSpeed += friction;
            }
            else if (xSpeed > 0)
            {
                xSpeed -= friction;
            }
        }

        if (f_leftStick_Y > 0 && ySpeed < maxSpeed)
        {
            ySpeed += aceleration*f_leftStick_Y;
        }
        else if (f_leftStick_Y < 0 && ySpeed > -maxSpeed)
        {
            ySpeed -= aceleration*-f_leftStick_Y;
        }
        else
        {
            if (ySpeed < 0)
            {
                ySpeed += friction;
            }
            else if (ySpeed > 0)
            {
                ySpeed -= friction;
            }
        }

        Vector3 movementVector = new Vector3(xSpeed, 0f,-ySpeed);
        playerRigidbody.MovePosition(transform.localPosition += movementVector * Time.deltaTime);




        //playerRigidbody.AddRelativeForce(f_leftStick_X * f_movementSpeed, 0f, f_leftStick_Y * f_movementSpeed);


        // Rotation
        var f_rightStick_X = Input.GetAxis("RightJoystickX_P" + joystickString);
        var f_rightStick_Y = Input.GetAxis("RightJoystickY_P" + joystickString);

        Vector3 rightStickInput = new Vector3(f_rightStick_X, f_rightStick_Y, 0.0f);
        if (rightStickInput.sqrMagnitude < 0.1f) {
            return;
        }
        var lookAngle = Mathf.Atan2(f_rightStick_X, f_rightStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngle, 0);
        //playerRigidbody.AddRelativeTorque(0f, lookAngle * f_movementSpeed, 0f);

    }
    

    void OnCollisionStay(Collision col) {
        if (col.gameObject.tag == "slippery")
        {
            isGrounded = true;
            GetComponent<Collider>().material.dynamicFriction = 0;
        }
        else {
            isGrounded = false;
        }
    }

    void PlayerHealthSystem() {
        if (f_playerDamage == 100) {
            Destroy(GameObject.Find("PlayerObj"));
        }
    }

}
