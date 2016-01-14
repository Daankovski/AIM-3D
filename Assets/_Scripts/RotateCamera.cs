using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {


    /// <summary>
    /// this is a script for the intro camera.
    /// </summary>


    private float speed = 1f;
    private float rads = 0f;
	void Start () {
        
        //sets up the camera in the position where it begins to move
        transform.position = new Vector3(0f,65f,-4f);
	}
	
	void Update () {
        rads += speed;
        if (rads <= 40)
        {
            //moves the camera based on the values.
            transform.Translate(new Vector3(0f, -speed/1.5f, 0f));
            if (rads > 20)
            {
                //slows down.
                speed -= 0.025f;
            }
        }
        
	}
}
