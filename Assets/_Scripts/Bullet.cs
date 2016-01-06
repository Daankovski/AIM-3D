using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject explosion;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * -speed;
    }
	void Update () {
	    
	}
    void OnCollisionEnter()
    {
        Destroy(this.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
