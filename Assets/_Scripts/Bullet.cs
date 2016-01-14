using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int AmountOfBounces= 1;

	void Start () {
        //gives the bullet a velocity forward based on their speed.
        GetComponent<Rigidbody>().velocity = transform.forward * -speed;
    }

    void OnCollisionEnter(Collision coll)
    {
        //when it hits something
        AmountOfBounces--;

        //if it doesn't has any more bounces left or comes across a player.
        if(AmountOfBounces <=0 || coll.gameObject.GetComponent<PlayerMovement>() != null)
        {
            //explodes!
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        
    }

    //getter and setter
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

}
