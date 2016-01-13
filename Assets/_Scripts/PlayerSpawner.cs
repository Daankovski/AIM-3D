using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField]
    private List<GameObject> players;
    [SerializeField]
    private int amountOfPlayers = 4;
    private GameObject endCam;
    private GameObject returnButton;

    void Start () {

        endCam = GameObject.Find("EndCamera");
        endCam.SetActive(false);
        returnButton = GameObject.Find("Canvas/ReturnButton");
        returnButton.SetActive(false);

        //initiates amountofplayers players that is given from the other scene.
        for (int i = 0; i < amountOfPlayers; i++)
        {
            Instantiate(players[i]);
        }
        if (amountOfPlayers < 4)
        {
            players.RemoveAt(3);
            if (amountOfPlayers < 3)
            {
                players.RemoveAt(2);
            }
        }
    }

    public void DeletePlayerFromList(string playerTag)
    {
        //checks which player has the player tag that needs to be removed from the list.
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.tag == playerTag)
            {
                players.RemoveAt(i);
            }
        }

        //if there is just one player remaining.
        if (players.Count == 1)
        {
            StartCoroutine(MakeEndScreen(playerTag));
        }
    }

    private IEnumerator MakeEndScreen(string playerTag)
    {
        //Changes the UI
        Text result = GameObject.Find("Canvas/Result").GetComponent<Text>() as Text;
        result.text = "End match!";
        GameObject.Find("Canvas/InGameUI").SetActive(false);
        
        //finds the one remaining player and removes the player controls over the player.
        GameObject winner = GameObject.Find(players[0].tag + "(Clone)");
        winner.GetComponent<PlayerMovement>().enabled = false;

        //waits for 2 seconds. 
        yield return new WaitForSeconds(2f);

        //puts  the winner in the middle of the arena.
        winner.transform.position = new Vector3(0f, 1.5f, 0f);
        winner.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

        //changes camera's 
        GameObject.Find("pivot camera").gameObject.SetActive(false);
        endCam.SetActive(true);
        returnButton.SetActive(true);

        //shows who is the winner.
        result.text = players[0].tag + " wins!";

        //The color of the text depends on the color of the player.
        switch (players[0].tag)
        {
            case "Player1":
                result.color = Color.green;
                break;
            case "Player2":
                result.color = Color.yellow;
                break;
            case "Player3":
                result.color = Color.red;
                break;
            case "Player4":
                result.color = Color.blue;
                break;
        }
    }
}
