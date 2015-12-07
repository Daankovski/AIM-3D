using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    [SerializeField]
    private int type;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "")
        {
            Debug.Log("you picked up a item");
            Destroy(col.gameObject);
            col.gameObject.GetComponent<PlayerMovement>().HasItem = true;
        }
    }
}
