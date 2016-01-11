using UnityEngine;
using System.Collections;

public class ArenaBoundaries : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player1");
        PlayerMovement player = Player1.GetComponent<PlayerMovement>();

        if (other.gameObject.tag == "Player1") { };
    }
    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerMovement>() != null) {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            Debug.Log("Speler is uit het veld");
            player.playerDamage = 100;
        }
    }
}
