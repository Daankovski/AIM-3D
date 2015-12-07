using UnityEngine;
using System.Collections;

public class PlayerMovementOLD : MonoBehaviour {
    
    private Rigidbody characterRigidbody;

    // Movement Related Variables
    private Vector3 movementVector;
    private float f_movementSpeed = 8f;
    private float f_jumpPower = 15f;
    private float f_gravity = 40f;
    private bool isGrounded = true;

    // Joystick Related Variables
    [SerializeField]
    private int i_joystickNumber;

    // Player Related Variables
    [SerializeField]
    private int f_playerHealth = 100;
	
	void Start () {
        characterRigidbody = GetComponent<Rigidbody>();
	}

    // Getters & Setters
    public int playerHealth {
        get { return f_playerHealth; }
        set { f_playerHealth = value; }
    }
	
	
	void FixedUpdate () {
        PlayerControlls();
        Debug.Log(playerHealth);
        Debug.Log(isGrounded);
    }

    void PlayerControlls() {
        // Horizontal & Vertical Movement
        string joystickString = i_joystickNumber.ToString();

        movementVector.x = Input.GetAxis("LeftJoystickX_P" + joystickString) * f_movementSpeed;
        movementVector.z = Input.GetAxis("LeftJoystickY_P" + joystickString) * f_movementSpeed;

        // Rotation
        var horz = Input.GetAxis("RightJoystickX_P" + joystickString);
        var vert = Input.GetAxis("RightJoystickY_P" + joystickString);
        //var aimDirection = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;
        Vector3 aimDirection = new Vector3(horz, 0f, vert);

        Debug.Log(aimDirection);

        // Jumping
        if (isGrounded)
        {
            movementVector.y = 0;
            if (Input.GetButton("A_P" + joystickString))
            {
                movementVector.y = f_jumpPower;
            }
        }

        movementVector.y -= f_gravity * Time.deltaTime;
        transform.Translate(movementVector * Time.deltaTime);
    }

    void PlayerDeath() {
        if (playerHealth <= 0) {
            Destroy(gameObject); // playerHealth word 0, maar object word niet verwijderd
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "slippery") {
            isGrounded = true;
        } 
    }
}
