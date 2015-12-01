using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Vector3 movementVector;
    private CharacterController characterController;
    private float f_movementSpeed = 8f;
    private float f_jumpPower = 15f;
    private float f_gravity = 40f;

    [SerializeField]
    private int i_joystickNumber;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement() {
        string joystickString = i_joystickNumber.ToString();
        movementVector.x = Input.GetAxis("LeftJoystickX_P" + joystickString) * f_movementSpeed;
        movementVector.z = Input.GetAxis("LeftJoystickY_P" + joystickString) * f_movementSpeed;

        if (characterController.isGrounded)
        {
            movementVector.y = 0;
            if (Input.GetButton("A_P" + joystickString))
            {
                movementVector.y = f_jumpPower;
            }
        }

        movementVector.y -= f_gravity * Time.deltaTime;
        characterController.Move(movementVector * Time.deltaTime);
    }
}
