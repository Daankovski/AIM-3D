using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int AmountOfBounces= 1;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * -speed;
    }
	void Update () {
	    
	}
    void OnCollisionEnter(Collision coll)
    {
        AmountOfBounces--;
        if(AmountOfBounces <=0 || coll.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        
    }
}
