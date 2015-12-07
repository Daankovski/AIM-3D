using UnityEngine;
using System.Collections;

public class ArenaBoundaries : MonoBehaviour {

    
    void Start () {
        
	}

	void Update () {
	
	}

    void OnTriggerExit(Collider other) {
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player1");
        PlayerMovement player = Player1.GetComponent<PlayerMovement>();
       
        if (other.gameObject.tag == "Player1") {
            Debug.Log("Speler is uit het veld");
            player.playerDamage = 100;
        }
    }
}
