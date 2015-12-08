using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody playerRigidbody;
    private GameObject player;

    private GameObject jumpPad;
    private jumpPadScript jumpPadScript;

    // Player Related Variables
    private Vector3 movementVector;
    private float f_movementSpeed = 8f;
    private bool isGrounded;
    [SerializeField]
    private float f_playerDamage = 0;

    private float mass = 20f;
    private Vector3 currentVelocity;
    private Vector3 currentPosition;
    private Vector3 currentTarget;

    // Joystick Related Variables & Related Code <--- overzetten naar nieuwe class
    [SerializeField]
    private int i_joystickNumber;
    private float f_leftStick_X;
    private float f_leftStick_Y;
    private float f_rightStick_X;
    private float f_rightStick_Y;


    void Start () {
        jumpPad = GameObject.Find(Tags.jumpPad);
        jumpPadScript = jumpPad.GetComponent<jumpPadScript>();

        playerRigidbody = GetComponent<Rigidbody>();
        player = this.gameObject;

        ControllerToPlayer();

        currentVelocity = new Vector3(0f, 0f, 0f);
        currentPosition = playerRigidbody.transform.position;
    }
	
	
	void Update () {
        PlayerMovementManager();


    }

    void FixedUpdate() {
        // Onderstaande tekst in void ControllerConfig
        string joystickString = i_joystickNumber.ToString();

        f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString);
        f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString);

        f_rightStick_X = Input.GetAxis("RightJoystickX_P" + joystickString);
        f_rightStick_Y = Input.GetAxis("RightJoystickY_P" + joystickString);

        float f_RightTrigger = Input.GetAxis("RightTrigger_P" + joystickString);
        float f_LeftTrigger = Input.GetAxis("LeftTrigger_P" + joystickString);

        if (f_RightTrigger > 0)
        {
            Debug.Log("Rechts");
        }
        if (f_LeftTrigger > 0)
        {
            Debug.Log("Links");
        }

        //Debug.Log(f_playerDamage);
        //Debug.Log(GetComponent<Collider>().material.dynamicFriction);
        

    }

    // Getters & Setters
    public float playerDamage {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
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
        
        // Horizontal & Vertical Movement
        movementVector.y = 0f;
        movementVector = new Vector3(f_leftStick_X * f_movementSpeed, movementVector.y, f_leftStick_Y * -f_movementSpeed);
        playerRigidbody.MovePosition(transform.localPosition += movementVector * Time.deltaTime);

        //playerRigidbody.AddRelativeForce(f_leftStick_X * f_movementSpeed, 0f, f_leftStick_Y * f_movementSpeed);
        
        /*
        // Horizontal & Vertical Movement w/ Velocity
        movementVector = new Vector3(f_leftStick_X , 0f, -f_leftStick_Y );
        Vector3 desiredStep = currentTarget - currentPosition;
        desiredStep.Normalize();

        Vector3 steeringForce = desiredStep * f_movementSpeed;

        currentVelocity += steeringForce / mass;

        currentPosition += currentVelocity * Time.deltaTime;
        transform.position = currentPosition;
        */

        // Rotation
        // w/ Right Stick to rotatie
        Vector3 rightStickInput = new Vector3(f_rightStick_X, f_rightStick_Y, 0.0f);
        if (rightStickInput.sqrMagnitude < 0.1f) {
            return;
        }
        var lookAngle = Mathf.Atan2(f_rightStick_X, f_rightStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngle, 0);

        // w/ Left Stick to rotate
        /*
        if (movementVector.sqrMagnitude < 0.1f)
        {
            return;
        }
        var lookAngleL = Mathf.Atan2(f_leftStick_X, f_leftStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngleL, 0);
        //playerRigidbody.AddRelativeTorque(0f, lookAngle * f_movementSpeed, 0f);
        */

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
            Destroy(GameObject.Find("PlayerObj"));
        }
    }

}
