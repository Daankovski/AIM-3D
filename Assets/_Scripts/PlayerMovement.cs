using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    [SerializeField]
    private int hasItem = 0;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private Text livesText;

    private bool isGrounded;
    [SerializeField]
    private float f_playerDamage = 0;

    // Player Related Variables
    private Vector3 movementVector;
    [SerializeField]
    private float f_movementSpeed;
    
    void Start()
    {
        jumpPad = GameObject.Find(Tags.str_jumpPad);
        jumpPadScript = jumpPad.GetComponent<jumpPadScript>();

        playerRigidbody = GetComponent<Rigidbody>();
        player = this.gameObject;

        controller = GetComponent<Controller>();

        playerRigidbody.mass = 10;
    }


    void Update()
    {
        PlayerMovementManager();
    }

    void FixedUpdate()
    {
        PlayerHealthSystem();
        PlayerMovementManager();
        if (Input.GetKeyDown(KeyCode.Space) && hasItem != 0)
        {
            Debug.Log("shoot!");
            hasItem = 0;
        }
    }

    // Getters & Setters
    public float playerDamage
    {
        get { return f_playerDamage; }
        set { f_playerDamage = value; }
    }

    public int HasItem
    {
        get { return hasItem; }
        set { hasItem = value; }
    }


    void PlayerMovementManager()
    {
        // Movement
        movementVector.y = 0f;
        movementVector = new Vector3(controller.LeftStick_X, 0, -controller.LeftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed, ForceMode.Impulse);
        movementVector = new Vector3(controller.LeftStick_X, movementVector.y * jumpPadScript.launchPower, -controller.LeftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed, ForceMode.VelocityChange);

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
            var lookAngle = Mathf.Atan2(controller.LeftStick_X, controller.LeftStick_Y) * Mathf.Rad2Deg;
            playerRigidbody.rotation = Quaternion.Euler(0, -lookAngleL, 0);

            if (controller.RightTrigger > 0)
            {
                f_movementSpeed = .5f;
            }
            else if (controller.LeftTrigger > 0)
            {
                f_movementSpeed = f_movementSpeed + 1f;
            }
            else
            {
                f_movementSpeed = 0f;
            }

            // Dodge
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
                        if (totalVeclocity < totalVeclocityOther)
                        {
                            f_playerDamage += totalVeclocityOther - totalVeclocity;
                            Debug.Log(f_playerDamage);
                        }
                        velocity *= 1f + f_playerDamage / 100f;
                    }
                }

    void OnCollisionStay(Collision col) {
                    if (col.gameObject.tag == "slippery")
                    {
                        isGrounded = true;
                    }
                    else
                    {
                        isGrounded = false;
                    }
                }

    void PlayerHealthSystem() {

                    if (f_playerDamage == 100)
                    {
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
                    if (lives == 0)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        transform.position = new Vector3(0f, 5f, 0f);
                    }
                }

}

        
    
