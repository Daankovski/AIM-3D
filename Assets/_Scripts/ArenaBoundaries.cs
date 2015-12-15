using UnityEngine;
using System.Collections;

public class ArenaBoundaries : MonoBehaviour {

    
    void Start () {
        
	}

	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerMovement>() != null) {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            Debug.Log("Speler is uit het veld");
            player.playerDamage = 100;
        }
    }
}
