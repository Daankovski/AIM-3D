using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

    private float speed = 2f;
    private float rads = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rads += speed;
        
        if (rads <= 360)
        {
            transform.Rotate(new Vector3(0f, speed, 0f));
            if (rads > 180)
            {
                speed -= 0.01f;
            }
        }
        
	}
}
