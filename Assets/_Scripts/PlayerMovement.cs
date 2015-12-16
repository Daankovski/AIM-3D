using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    ///////////////////
    /// f_ == float
    /// i_ == int
    /// str_ == string
    //////////////////
 

    Rigidbody playerRigidbody;

    [HideInInspector]
    public GameObject player;

    GameObject jumpPad;
    jumpPadScript jumpPadScript;

    Controller controller;

    // Player Related Variables
    private Vector3 movementVector;
    [SerializeField]
    private float f_movementSpeed;
    [SerializeField]
    private float f_playerDamage = 0;

    void Start () {
        jumpPad = GameObject.Find(Tags.str_jumpPad);
        jumpPadScript = jumpPad.GetComponent<jumpPadScript>();

        playerRigidbody = GetComponent<Rigidbody>();
        player = this.gameObject;
        
        controller = GetComponent<Controller>();

        playerRigidbody.mass = 10;
        
    }
	
	
	void Update () {
        PlayerMovementManager();
    }

    void FixedUpdate() {    

    }

    // Getters & Setters
    public float playerDamage {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
    }
       
    void PlayerMovementManager() {
        
        // Movement
        movementVector.y = 0f;
        movementVector = new Vector3(controller.LeftStick_X, 0, -controller.LeftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed, ForceMode.Impulse);
       
        // Rotation     
        if (movementVector.sqrMagnitude < 0.1f)
        {
            return;
        }
        var lookAngleL = Mathf.Atan2(controller.LeftStick_X, controller.LeftStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngleL, 0);
        

        // Boost
        if (controller.RightTrigger > 0)
        {
            Debug.Log("Rechts");
            f_movementSpeed = 4f;
        }
        else if (controller.LeftTrigger > 0)
        {
            Debug.Log("Links");
            f_movementSpeed = f_movementSpeed + 10f;
        }
        else {
            f_movementSpeed = 0f;
        }

        // Dodge
        
        


    }

    void PlayerHealthSystem() {
        if (f_playerDamage == 100) {
            Destroy(GameObject.Find("PlayerObj"));
        }
    }

}
