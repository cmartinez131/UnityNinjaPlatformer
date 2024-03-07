using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicScript : MonoBehaviour
{
    //reference to the player game object
    public GameObject player;

    public int playerScore;
    public TextMeshProUGUI playerScoreLabel;

    //list to keep track of all the coin gameObjects
    public List<GameObject> coins;

    // public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        // find all coins in the scene and add them to the list of coin gameObjects
        coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));
        UpdateScoreDisplay();
    }

    public void IncrementScore(int amount)
    {
        Debug.Log("Incrementing score by: " + amount);
        playerScore += amount;
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
            playerScoreLabel.text = playerScore.ToString();
    }

    public void RestartGame()
    {
        playerScore = 0;
        UpdateScoreDisplay();
        //send the players back to starting position
        ResetPlayerPosition();
        ReactivateCoins();  //reactivate coind on a new round
    }

    private void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = new Vector2(5, 1);
        }
        else
        {
            Debug.LogError("Player gameobject not asigned in logicScipt");
        }
    }

    private void ReactivateCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true); //reactivate each coin in the scene
        }
    }
    
}
