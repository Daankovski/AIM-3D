using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody playerRigidbody;
    private GameObject player;

    // Player Related Variables
    private float f_movementSpeed = 8f;
    private bool isGrounded;
    private float f_playerDamage = 0;

    // Joystick Related Variables
    [SerializeField]
    private int i_joystickNumber;
    private string joystickString;

    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerObj");

        ControllerToPlayer();
    }
	
	
	void Update () {
        PlayerMovementManager();
    }

    void FixedUpdate() {
        Debug.Log(f_playerDamage);
    }

    // Getters & Setters
    public float playerDamage {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
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
        var f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString) * f_movementSpeed;
        var f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString) * -f_movementSpeed;

        Vector3 movementVector = new Vector3(f_leftStick_X, 0f, f_leftStick_Y);
        playerRigidbody.MovePosition(transform.localPosition += movementVector * Time.deltaTime);
   
        // Rotation
        var f_rightStick_X = Input.GetAxis("RightJoystickX_P" + joystickString);
        var f_rightStick_Y = Input.GetAxis("RightJoystickY_P" + joystickString);

        Vector3 rightStickInput = new Vector3(f_rightStick_X, f_rightStick_Y, 0.0f);
        if (rightStickInput.sqrMagnitude < 0.1f) {
            return;
        }
        var lookAngle = Mathf.Atan2(f_rightStick_X, f_rightStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngle, 0);

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
            Destroy(this.gameObject);
        }
    }

}
