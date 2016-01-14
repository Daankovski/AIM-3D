using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    //the "type" decides which kind of item the player will get.
    [SerializeField]
    private int type = 1;

    [SerializeField]
    private GameObject pickedUpEffect;
    private GameObject pickUpSpawner;

	void Start () {
        //when an pickup is made, it will produce a little explosion and has a little force upwards.
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 5f, 0f);

        pickUpSpawner = GameObject.Find("pick up spawner");
	}
	void Update()
    {
        //when the item falls down.
        if(transform.position.y < -20)
        {
            Destroy(this.gameObject);
            pickUpSpawner.GetComponent<ItemSpawner>().CurrentAmountOfItems--;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        //if an object with a playerschript (the players) touches this without having an item, the player will get an item.
        if (col.gameObject.GetComponent<PlayerMovement>() != null && col.gameObject.GetComponent<PlayerMovement>().HasItem == 0)
        {
            //makes an particle effect.
            pickedUpEffect.transform.position = col.gameObject.transform.position;
            Instantiate(pickedUpEffect);
            //destoys this object.
            Destroy(this.gameObject);
            
            //updates the amount of items.
            pickUpSpawner.GetComponent<ItemSpawner>().CurrentAmountOfItems--;

            //updates the item the player now has.
            col.gameObject.GetComponent<PlayerMovement>().HasItem = type;
        }
    }
}
