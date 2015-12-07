using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private bool hasItem = false;

    private Rigidbody playerRigidbody;
    private GameObject player;

    // Player Related Variables
    private float f_movementSpeed = 8f;
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


    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerObj");

        ControllerToPlayer();
    }
	
	
	void Update () {
        f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString);
        f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString);

        if (Input.GetKeyDown(KeyCode.Space) && hasItem)
        {
            Debug.Log("shoot!");
            hasItem = false;
        }
    }

    void FixedUpdate() {
        //Debug.Log(f_playerDamage);
        Debug.Log(GetComponent<Collider>().material.dynamicFriction);
        PlayerMovementManager();
    }

    // Getters & Setters
    public float playerDamage {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
    }
    public bool HasItem
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
        
        Vector3 movementVector = new Vector3(f_leftStick_X * f_movementSpeed, 0f, f_leftStick_Y * -f_movementSpeed);
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
