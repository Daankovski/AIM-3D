using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private CharacterController characterController;

    // Movement Related Variables
    private Vector3 movementVector;
    private float f_movementSpeed = 8f;
    private float f_jumpPower = 15f;
    private float f_gravity = 40f;
    private float f_friction;
    private float f_velocity = 1f;

    // Joystick Related Variables
    [SerializeField]
    private int i_joystickNumber;

    // Player Related Variables
    [SerializeField]
    private int f_playerHealth = 100;
	
	void Start () {
        characterController = GetComponent<CharacterController>();
	}

    // Getters & Setters
    public int playerHealth {
        get { return f_playerHealth; }
        set { f_playerHealth = value; }
    }
	
	
	void Update () {
        PlayerControlls();
    }

    void FixedUpdate() {
        Debug.Log(playerHealth);
    }

    void PlayerControlls() {
        // Horizontal & Vertical Movement
        string joystickString = i_joystickNumber.ToString();

        float f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString) * f_movementSpeed;
        float f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString) * -f_movementSpeed;

        movementVector = new Vector3(f_leftStick_X, movementVector.y, f_leftStick_Y);

        // Rotation
        var horz = Input.GetAxis("RightJoystickX_P" + joystickString);
        var vert = Input.GetAxis("RightJoystickY_P" + joystickString);
        var aimDirection = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, aimDirection, 0);

        // Jumping
        if (characterController.isGrounded)
        {
            movementVector.y = 0;
            if (Input.GetButton("A_P" + joystickString))
            {
                movementVector.y = f_jumpPower;
            }
        }

        movementVector.y -= f_gravity * Time.deltaTime;
        characterController.Move(movementVector * Time.deltaTime);
    }

    void PlayerDeath() {
        if (playerHealth <= 0) {
            Destroy(this.gameObject);
        }
    }
}
