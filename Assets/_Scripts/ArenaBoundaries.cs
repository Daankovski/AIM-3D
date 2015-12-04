using UnityEngine;
using System.Collections;

public class ArenaBoundaries : MonoBehaviour {

    
    void Start () {
        
	}

	void Update () {
	
	}

    void OnTriggerExit(Collider other) {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
       
        if (other.gameObject.tag == "Player") {
            Debug.Log("Speler is uit het veld");
            player.playerHealth = 0;
        }
    }
}
