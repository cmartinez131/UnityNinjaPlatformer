using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    // handles UI logic specific to the pause menu such as showing/hiding the menu and toggling settings

    //tag reference to another panel
    //public GameObject settingsPanel; //assign this in instpector

    //public Canvas canvas;

    //private void Start()
    //{
    //    // Ensure the pause menu is not active when the game starts
    //    canvas.enabled = false;
    //}

    //public void ResumeGame()
    //{
    //    Debug.Log("resume game button pressed");
    //    //gameObject.SetActive(false);
    //    //Time.timeScale = 1f; // resume normal time flow
    //}

    //public void OpenSettings()
    //{
    //    Debug.Log("settings button on pause menu pressed");
    //    //settingsPanel.SetActive(true); // show settings panel
    //}

    //public void QuitGameButtonPressed()
    //{
    //    Debug.Log("quit game button pressed");
    //}

    //public void ReturnToMainMenu()
    //{
    //    //Time.timeScale = 1f; // ensure time flow is resumed
    //    SceneManager.LoadScene("SampleScene");
    //}

    //public void TogglePauseMenu()
    //{
    //    canvas.enabled = !canvas.enabled;
    //}

}
