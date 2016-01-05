using UnityEngine;
using System.Collections;

public class jumpPadScript : MonoBehaviour {

<<<<<<< HEAD
    /// <summary>
    /// f_ == float
    /// i_ == int
    /// str_string
    /// </summary>

=======
>>>>>>> Nathan
    [SerializeField]
    private float f_launchPower = 0f;


    // Getters & Setters
    public float launchPower {
        get { return f_launchPower; }
        set { f_launchPower = value; }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player1") {
            launchPower = 10f;
        }
    }	
	
}
