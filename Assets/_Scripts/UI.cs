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

    private float delay = 0f;
    private int selectedObj = 1;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
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
    public int LivesValue {
        get { return i_livesValue; }
    }

    public int PlayersValue {
        get { return i_playerValue; }
    }

    public bool PowerUps {
        get { return powerUps.isOn; }
    }

    void FixedUpdate() {
        UpdateUI();
    }

    void UpdateUI() {
        if(Mathf.Round(Input.GetAxis("MenuVertical")) > 0)
        {

        }

        switch (i_playerValue) {
            case 2:
                if (Mathf.Round(Input.GetAxis("MenuHorizontal")) == 1) {
                    i_playerValue += 1;
                }
            break;
            case 3:
                if (Mathf.Round(Input.GetAxis("MenuHorizontal")) == 1)
                {
                    i_playerValue += 1;
                }
                else if (Mathf.Round(Input.GetAxis("MenuHorizontal")) == -1) {
                    i_playerValue -= 1;
                }
            break;
        }

        if (i_livesValue >= 5)
        {
            if (Mathf.Round(Input.GetAxis("MenuHorizontal")) == 1)
            {
                i_livesValue += 1;
            }
            else if (Mathf.Round(Input.GetAxis("MenuHorizontal")) == -1)
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
                if (Input.GetButton("Select") && delay == 0f)
                {
                    powerUps.isOn = false;
                    StartCoroutine(DelayController());
                }                
                break;
            case false:
                if (Input.GetButton("Select") && delay == 0f) {
                    powerUps.isOn = true;
                    StartCoroutine(DelayController()); 
                }
                break;
        }
    }

    IEnumerator DelayController() {
        delay = .125f;
        yield return new WaitForSeconds(delay);
        delay = 0f;
    }
}
