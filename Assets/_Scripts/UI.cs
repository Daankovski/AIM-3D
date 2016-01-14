using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider playerSlider;
    private int i_playerValue;
    private Text playerValueText;

    [SerializeField]
    private Slider livesSlider;
    private int i_livesValue;
    private Text livesValueText;

    [SerializeField]
    private Toggle powerUps;

    // Use this for initialization
    void Start()
    {
        playerSlider = GameObject.Find("PlayerSlider").GetComponent<Slider>();
        i_playerValue = (int)playerSlider.minValue;
        playerSlider.value = i_playerValue;
        playerValueText = GameObject.Find("PlayerValue").GetComponent<Text>();

        livesSlider = GameObject.Find("LivesSlider").GetComponent<Slider>();
        i_livesValue = (int)livesSlider.minValue;
        livesSlider.value = i_livesValue;
        livesValueText = GameObject.Find("LivesValue").GetComponent<Text>();

        powerUps = GameObject.Find("PowerUps").GetComponent<Toggle>();
        powerUps.isOn = false;


    }

    // getters & setters
    public int TotalLivesValue {
        get { return i_livesValue; }
    }

    public int TotalPlayersValue {
        get { return i_playerValue; }
    }

    void FixedUpdate() {
        UpdateUI();
        DebugLog();
    }

    void UpdateUI() {
        switch (i_playerValue) {
            case 2:
                if (Input.GetAxis("MenuHorizontal") == 1) {
                    i_playerValue += 1;
                }
            break;
            case 3:
                if (Input.GetAxis("MenuHorizontal") == 1)
                {
                    i_playerValue += 1;
                }
                else if (Input.GetAxis("MenuHorizontal") == -1) {
                    i_playerValue -= 1;
                }
            break;
        }

        if (i_livesValue >= 5)
        {
            if (Input.GetAxis("MenuHorizontal") == 1)
            {
                i_livesValue += 1;
            }
            else if (Input.GetAxis("MenuHorizontal") == -1)
            {
                i_livesValue -= 1;
            }
        }
        else
        {
            i_livesValue = 0;
        }

        playerValueText.text = "" + playerSlider.value;
        livesValueText.text = "" + livesSlider.value;

        switch (powerUps.isOn) {
            case true:
                if (Input.GetButton("Select"))
                {
                    powerUps.isOn = false;
                }
                break;
            case false:
                if (Input.GetButtonDown("Select")) {
                    powerUps.isOn = true;
                }
                break;

        }

    }

    void DebugLog() {
        
    }
}
