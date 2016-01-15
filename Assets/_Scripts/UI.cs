using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider playerSlider;
    [SerializeField]
    private float i_playerValue;
    private Text playerValueText;

    [SerializeField]
    private Slider livesSlider;
    [SerializeField]
    private float i_livesValue;
    private Text livesValueText;

    [SerializeField]
    private Slider itemSlider;
    [SerializeField]
    private float itemValue;
    private Text itemValueText;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        playerSlider = GameObject.Find("PlayerSlider").GetComponent<Slider>();
        playerSlider.value = i_playerValue;
        playerValueText = GameObject.Find("PlayerValue").GetComponent<Text>();

        livesSlider = GameObject.Find("LivesSlider").GetComponent<Slider>();
        livesSlider.value = i_livesValue;
        livesValueText = GameObject.Find("LivesValue").GetComponent<Text>();

        itemSlider = GameObject.Find("PowerUpsSlider").GetComponent<Slider>();
        itemSlider.value = itemValue;
        itemValueText = GameObject.Find("PowerUpsValue").GetComponent<Text>();

    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    // getters & setters
    [SerializeField]
    public float LivesValue {
        get { return i_livesValue; }
    }
    [SerializeField]
    public float PlayersValue
    {
        get { return i_playerValue; }
    }

    [SerializeField]
    public float ItemValue
    {
        get { return itemValue; }
    }

    void Update() {
        UpdateUI();
    }

    void UpdateUI() {
        if (playerSlider != null)
        {
            i_playerValue = playerSlider.value;
            playerValueText.text = "" + playerSlider.value;
            if (i_playerValue == 2)
            {
                playerValueText.text = "  " + playerSlider.value + " >";
            }
            else if (i_playerValue == 4)
            {
                playerValueText.text = "< " + playerSlider.value + "  ";
            }
            else
            {
                playerValueText.text = "< " + playerSlider.value + " >";
            }

            i_livesValue = livesSlider.value;
            if (i_livesValue == 1)
            {
                livesValueText.text = "  " + livesSlider.value + " >";
            }
            else if (i_livesValue == 5)
            {
                livesValueText.text = "< " + livesSlider.value + "  ";
            }
            else
            {
                livesValueText.text = "< " + livesSlider.value + " >";
            }



            itemValue = itemSlider.value;
            if (itemValue == 0)
            {
                itemValueText.text = "  " + "On" + " >";
            }
            else
            {
                itemValueText.text = "< " + "Off" + "  ";
            }

        }

    }
}
