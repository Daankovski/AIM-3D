using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

    /// 
    /// f_ == float
    /// i_ == int
    /// str_string
    /// 

    PlayerMovement player;

    private float f_leftStick_X;
    private float f_leftStick_Y;
    private float f_rightStick_X;
    private float f_rightStick_Y;
    private float f_rightTrigger;
    private float f_leftTrigger;
    private float f_A;
   
    [SerializeField]
    private int i_joystickNumber;

    void Start() {
        player = GetComponent<PlayerMovement>();
    }


    void ControllerToPlayer()
    { // Makes it so that the playerTag assigned to an object equals the joystickNumber of the Controller
        switch (player.player.tag)
        {
            case Tags.str_player1:
                i_joystickNumber = 1;
                break;
            case Tags.str_player2:
                i_joystickNumber = 2;
                break;
            case Tags.str_player3:
                i_joystickNumber = 3;
                break;
            case Tags.str_player4:
                i_joystickNumber = 4;
                break;
            default:
                i_joystickNumber = 0;
                break;
        }
    }

    // Getters & Setters
       public float LeftStick_Y
    {
        get { return f_leftStick_Y; }
        set { f_leftStick_Y = value; }
    }

    public float LeftStick_X {
        get { return f_leftStick_X; }
        set { f_leftStick_X = value; }
    }

    public float RightStick_X
    {
        get { return f_rightStick_X; }
        set { f_rightStick_X = value; }
    }

    public float RightStick_Y
    {
        get { return f_rightStick_Y; }
        set { f_rightStick_Y = value; }
    }

    public float RightTrigger
    {
        get { return f_rightTrigger; }
        set { f_rightTrigger = value; }
    }

    public float LeftTrigger
    {
        get { return f_leftTrigger; }
        set { f_leftTrigger = value; }
    }

    public float A
    {
        get { return f_A; }
        set { f_A = value; }
    }


    void Update()
    {
        ControllerToPlayer();

        string joystickString = i_joystickNumber.ToString();

        f_leftStick_X = Input.GetAxis("LeftJoystickX_P" + joystickString);
        f_leftStick_Y = Input.GetAxis("LeftJoystickY_P" + joystickString);

        f_rightStick_X = Input.GetAxis("RightJoystickX_P" + joystickString);
        f_rightStick_Y = Input.GetAxis("RightJoystickY_P" + joystickString);

        f_rightTrigger = Input.GetAxis("RightTrigger_P" + joystickString);
        f_leftTrigger = Input.GetAxis("LeftTrigger_P" + joystickString);

        f_A = Input.GetAxis("A_P" + joystickString);
    }
}

