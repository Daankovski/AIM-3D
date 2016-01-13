using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField]
    private float speed = 1f;

	void Update () {
        //rotates the object arround the Y-as.
        transform.Rotate(0f, speed, 0f);
	}
}
