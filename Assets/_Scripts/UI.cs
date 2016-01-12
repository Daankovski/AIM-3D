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
    }

    // getters & setters
    public int TotalLivesValue {
        get { return i_livesValue; }
    }

    public int TotalPlayersValue {
        get { return i_playerValue; }
    }

    void FixedUpdate() {
        UpdateSlider();
        DebugLog();
    }

    void UpdateSlider() {
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


    }

    void DebugLog() {
        
    }
}
