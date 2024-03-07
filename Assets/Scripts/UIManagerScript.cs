using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  //for accesssing scene management

public class UIManagerScript : MonoBehaviour
{

    public TextMeshProUGUI currentSceneLabel;    //assign in inspector
    public TextMeshProUGUI currentStateLabel;

    // array to store a list of states in order

    public void UpdateCurrentSceneLabel(string sceneName)
    {
        if (currentSceneLabel != null)
        {
            currentSceneLabel.text = "Current Scene: " + sceneName;
        }
        else
        {
            Debug.Log("CurrentSceneLabel not assigned");
        }
    }

    public void UpdateCurrentStateLabel(string state)
    {
        if (currentStateLabel != null)
        {
            currentStateLabel.text = "Current State: " + state;
        }
        else
        {
            Debug.LogError("currentStateLabel not assigned");
        }
    }

    // method to toggle the state by calling NextState() from the GameManager instance
    // then the game manager will call this script again to update the label
    public void ToggleState()
    {
        GameManager.Instance.NextState();
    }


    // toggle button to turn off panels for testing
    public void TogglePanelButtonPressed()
    {
        Debug.Log("Toggle button pressed");
        UpdateCurrentSceneLabel("button");
    }

   

}
