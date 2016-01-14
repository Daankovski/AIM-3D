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

    // GameObject Related Variables
    [SerializeField]
    private int hasItem = 0;
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject miniBullet;
    [SerializeField]
    private GameObject lineBullet;
    private int amountOfMiniBullets = 10;
    private Vector3 shootPos;

    private GameObject shield;

    // UI Related Variables
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text damageText;
    [SerializeField]
    private float damage = 0;

    private Rigidbody playerRigidbody;

    [HideInInspector]
    public GameObject player;

    private GameObject spawnPos;
    [SerializeField]
    private GameObject spawnEffect;

    [SerializeField]
    private GameObject bounceEffect;

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
        spawnPos = GameObject.Find("SpawnPositions/Spawn_"+gameObject.tag);
        livesText = GameObject.Find("Canvas/InGameUI/lives_" + gameObject.tag).GetComponent<Text>() as Text;
        damageText = GameObject.Find("Canvas/InGameUI/damage_" + gameObject.tag).GetComponent<Text>() as Text;
        transform.position = spawnPos.transform.position;
        Instantiate(spawnEffect, transform.position, Quaternion.identity);
        UpdateLifeText();
        UpdateDamageText();
        jumpPad = GameObject.Find(Tags.str_jumpPad);
        jumpPadScript = jumpPad.GetComponent<jumpPadScript>();

        playerRigidbody = GetComponent<Rigidbody>();
        player = this.gameObject;

        controller = GetComponent<Controller>();

        playerRigidbody.mass = 10;
    }


    void Update()
    {
        PlayerControlls();
    }

    void FixedUpdate()
    {
        PlayerHealthSystem();
        PlayerControlls();
        
        if (hasItem == 2 )
        {
            hasItem = 0;
            StartCoroutine(Shield());
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

    void PlayerControlls()
    {
        // Movement   
        movementVector = new Vector3(controller.LeftStick_X, 0.0f, -controller.LeftStick_Y);
        playerRigidbody.AddForce(movementVector * f_movementSpeed, ForceMode.Force);

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
            f_movementSpeed = 20f;
        }
        else if (controller.LeftTrigger > 0) //Left Trigger is boost
        {
            f_movementSpeed = f_movementSpeed + 5f;
            
        }
        else
        {
            f_movementSpeed = 0f;
        }

        // Dodge

        // Buttons
        if (controller.A != 0)
        {
            Items();
        }
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
                damage -= Mathf.RoundToInt((totalVeclocityOther - totalVeclocity)/5);
                Instantiate(bounceEffect, transform.position, Quaternion.identity);
                if (damage > 100)
                {
                    damage = 99;
                }
                UpdateDamageText();
            }
            GetComponent<Rigidbody>().mass = 10f - damage/20f;
        }
        else if(col.gameObject.GetComponent<Bullet>() != null )
        {
            damage += col.gameObject.GetComponent<Bullet>().Speed/10 * col.gameObject.GetComponent<Rigidbody>().mass;
            if (damage > 100)
            {
                damage = 99;
            }
            UpdateDamageText();
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
            StartCoroutine(Die());
            
        }
    }

    void Items() {
        if (hasItem == 1)
        {
            shootPos = transform.Find("shootposition").transform.position;
            Instantiate(bullet, shootPos, transform.Find("shootposition").rotation);
        }
        else if (hasItem == 2)
        {
            hasItem = 0;
            StartCoroutine(Shield());
        }
        else if (hasItem == 3)
        {
            StartCoroutine(RapidFire(0));
        }
        else if (hasItem == 4)
        {
            shootPos = transform.Find("shootposition").transform.position;
            Instantiate(lineBullet, shootPos, transform.Find("shootposition").rotation);
        }
        hasItem = 0;
    }

    IEnumerator Die()
    {
        lives--;
        UpdateLifeText();
        damage = 0;
        UpdateDamageText();
        GetComponent<Rigidbody>().mass = 1f - damage / 200f;

        //creates an explosion if the player dies.
        explosion.transform.position = transform.position;
        Instantiate(explosion);

        //for the duration the player is not visible while it still exists.
        transform.position = new Vector3(0f, 1000f, 0f);

        //checks if the player has still lives left to respawn.
        if(lives ==0)
        {
            Destroy(this.gameObject);
            GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().DeletePlayerFromList(gameObject.tag);
        }
        else
        {
            //waits for 2 seconds.
            yield return new WaitForSeconds(2f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().mass = 10f;
            transform.position = spawnPos.transform.position;
            Instantiate(spawnEffect, transform.position, Quaternion.identity);
        }
    }
    void UpdateLifeText()
    {
        if(lives > 0)
        {
            livesText.text = "li ves: " + lives;
            if(lives == 1)
            {
                livesText.color = Color.yellow;
            }
        }
        else
        {
            livesText.color = Color.red;
            livesText.text = "Defeated!";
        }
        
    }
    void UpdateDamageText()
    {
        damageText.text = damage + "%";
        damageText.fontSize = Mathf.RoundToInt(30 + damage / 3f);
        damageText.color = new Color(1f,1f-damage/100f,1f-damage/100f);
    }
    IEnumerator Shield()
    {
        transform.FindChild("shield").gameObject.SetActive(true);
        GetComponent<Rigidbody>().mass *= 3;
        yield return new WaitForSeconds(5f);
        transform.FindChild("shield").gameObject.SetActive(false);
        GetComponent<Rigidbody>().mass /= 3;
    }
    IEnumerator RapidFire(int i)
    {
        shootPos = transform.Find("shootposition").transform.position;
        Instantiate(miniBullet, shootPos, transform.Find("shootposition").rotation);
        yield return new WaitForSeconds(0.1f);
        if(i <amountOfMiniBullets)
        {
            i++;
            StartCoroutine(RapidFire(i));
            
        }
    }
}


        
    
