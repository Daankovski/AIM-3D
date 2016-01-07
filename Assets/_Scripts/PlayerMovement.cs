using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    ///////////////////
    /// f_ == float
    /// i_ == int
    /// str_ == string
    //////////////////

    Controller controller;

    [SerializeField]
    private int hasItem = 0;
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject bullet;
    private Vector3 shootPos;
    [SerializeField]
    private int lives = 10;
    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text damageText;
    [SerializeField]
    private float damage = 0;

    private Rigidbody playerRigidbody;

    [HideInInspector]
    public GameObject player;

    private GameObject jumpPad;
    private jumpPadScript jumpPadScript;


    private bool isGrounded;
    [SerializeField]
    private float f_playerDamage = 0;

    // Player Related Variables
    private Vector3 movementVector;
    [SerializeField]
    private float f_movementSpeed;
 
    void Start () {
        
        damageText.text = damage + "%";

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

            shootPos = transform.Find("shootposition").transform.position;
            Instantiate(bullet, shootPos, transform.Find("shootposition").rotation);
            hasItem = 0;
        }
    }

    // Getters & Setters
    public float playerDamage {
        get { return damage; }
        set { damage = value; }
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
        
        movementVector = new Vector3(controller.LeftStick_X, movementVector.y * jumpPadScript.launchPower, -controller.LeftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed, ForceMode.VelocityChange);

        // Rotation     
        if (movementVector.sqrMagnitude < 0.1f)
        {
            return;
        }
        var lookAngleL = Mathf.Atan2(controller.LeftStick_X, controller.LeftStick_Y) * Mathf.Rad2Deg;
        playerRigidbody.rotation = Quaternion.Euler(0, -lookAngleL, 0);



        //Triggers
        if (controller.RightTrigger > 0)
        {
            f_movementSpeed = .5f;
        }
        else if (controller.LeftTrigger > 0) //Left Trigger is boost
        {
            f_movementSpeed = f_movementSpeed + .15f;
        }
        else
        {
            f_movementSpeed = 0f;
        }

            // Dodge
        }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            float totalVeclocity = Mathf.Round(Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z));
            Vector3 velocityOther = col.gameObject.GetComponent<Rigidbody>().velocity;
            float totalVeclocityOther = Mathf.Round(Mathf.Abs(velocityOther.x) + Mathf.Abs(velocityOther.z));
            
            if(totalVeclocity >= totalVeclocityOther)
            {
                Debug.Log("vel: " + totalVeclocity +": "+ totalVeclocityOther);
                damage -= totalVeclocityOther - totalVeclocity;
                damageText.text =  damage+"%";
                if (damage > 100)
                {
                    damage = 100;
                }
            }
            GetComponent<Rigidbody>().mass = 1f - damage/200f;
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
        if (damage == 100) {
            //if player dies
            damage = 0;
            
            StartCoroutine(Die());
            
        }
    }

    IEnumerator Die()
    {
        lives--;
        livesText.text = "lives: " + lives;
        damage = 0;
        GetComponent<Rigidbody>().mass = 1f - damage / 200f;
        damageText.text = damage + "%";
        //creates an explosion if the player dies.
        explosion.transform.position = transform.position;
        Instantiate(explosion);

        //for the duration the player is not visible while it still exists.
        transform.position = new Vector3(0f, 1000f, 0f);

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

        
    
