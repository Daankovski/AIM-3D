using UnityEngine;
using System.Collections;

public class ArenaBoundaries : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerMovement>() != null) {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.playerDamage = 100;
        }
    }
}
