using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoalColliderScript : MonoBehaviour
{
    // variable to store reference to the logic script
    public LogicScript logic;
    private bool isScored = false; // to check if the score has already been incremented

    void Start()
    {
        // get reference to logic script
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure that the player only scores once per hitting the goal.
        // ran into an error because the player gameobject has two colliders and it would increment score twice
        if (collision.gameObject.CompareTag("Player") && !isScored)
        {
            //logic.IncrementScore(1);
            logic.RestartGame();
            isScored = true;
            // Optionally, disable the collider to prevent further score increments
            // GetComponent<Collider2D>().enabled = false;
        }
    }

    // can reset `isScored` when appropriate, for example, when the player respawns or leaves the goal area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isScored = false;
        }
    }

     // Optionally, re-enable the collider if you disabled it before
            // GetComponent<Collider2D>().enabled = true;

}
