using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlatformScript : MonoBehaviour
{

    public float timeToLoad = 5f; // time player must stand on platform to go to level
    public float timeCounter = 0f;
    private bool playerOnPlatform = false;

    public string nextLevel; //name of next level scene to load


    // Update is called once per frame
    void Update()
    {

        if (playerOnPlatform)
        {
            timeCounter += Time.deltaTime;

            if (timeCounter >= timeToLoad)
            {
                // use GameManager to load the next level
                GameManager.Instance.LoadLevel(nextLevel);
                //LoadNextLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(timeToLoad);
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            timeCounter = 0f; //reset counter when player steps on the platform
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            timeCounter = 0f;   // reset counter when player leaves the platform
        }
    }

    //private void LoadNextLevel()
    //{
    //    SceneManager.LoadScene(nextLevel); //load scene with this name
    //}
}
