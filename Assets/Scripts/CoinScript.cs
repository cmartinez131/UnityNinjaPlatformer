using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Coin collected by " + other.gameObject.name);
        logic.IncrementScore(1);
        gameObject.SetActive(false); //deactivate the coin on collision
    }

}
