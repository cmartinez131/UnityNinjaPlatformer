using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonHandler : MonoBehaviour
{

    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button quitGameButton;


    void Start()
    {
        resumeButton.onClick.AddListener(() => GameManager.Instance.TogglePauseMenu());
        settingsButton.onClick.AddListener(() => Debug.Log("Settings button clicked"));
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.GoToMenuScene());
        quitGameButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}
