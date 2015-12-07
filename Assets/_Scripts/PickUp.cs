using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    [SerializeField]
    private int type;
    [SerializeField]
    private GameObject pickedUpEffect;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() != null && col.gameObject.GetComponent<PlayerMovement>().HasItem == false)
        {
            Debug.Log("you picked up a item");
            pickedUpEffect.transform.position = col.gameObject.transform.position;
            Instantiate(pickedUpEffect);
            
            Destroy(this.gameObject);
            col.gameObject.GetComponent<PlayerMovement>().HasItem = true;
        }
    }
}
