using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// to do: move scene management to its own place
// to do: move pause menu logic to its own place 

public class GameManager : MonoBehaviour
{

    // this loads when the game is first started
    // initialize any data before the player begins the game
    // Main Menu contains options to start the game, adjust settings, view credits, load save files


    // use of singleton pattern to make sure there is only one instance 'Instance' of GameManager class
    // any other script can reference 'GameManager.Instance' to access the single instance
    // 'static' means that it belongs to the class itself rather than an object instance of it
    // in general, static variables are shared by all instances of the class
    // public getter and private setter. other classes can read Instance
    // but only class itself can assign properties to it

    public static GameManager Instance { get; private set; }

    // this script manages game-wide operations like quitting the game, loading levels,
    // handling game state (like paused, running), and other global behaviors

    public GameObject pauseMenuPrefab;
    private GameObject pauseMenuInstance;

    // get a reference to the UIManager prefab 
    public GameObject UIManagerPrefab;
    //instantiate a UIManager prefab instance
    private GameObject UIManagerInstance;

    //get the reference to the instance of the uimanager
    //need to do it dynamically(progmatically) at runtime because the object is instantiated at runtime
    // we can access uiManager through this reference
    private UIManagerScript uiManager;

    
    //awake is called when the script instance is loaded
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // make this obejct persists across scenes
            InstantiatePauseMenu();
            InstantiateUIManager(); //instantiate UIManager object
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // make sure there are no duplicate GameManagers
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();

        }
    }

    public string[] gameStates = { "ItemSelection", "ItemPlacement", "Gameplay", "ShowResults", "EndLevel" };
    public int currentStateIndex = 0;

    // cycle through the states
    public void NextState()
    {
        currentStateIndex = (currentStateIndex + 1) % gameStates.Length;
        UpdateUIManagerStateLabel();
    }
    // note -> if i go through all the states, then that means the round shoudl restard

    private void UpdateUIManagerStateLabel()
    {
        if (uiManager != null)
        {
            uiManager.UpdateCurrentStateLabel(gameStates[currentStateIndex]);
        }
    }

    // logic for game manager to keep track of current round, number of rounds,
    // and the scores for each round

    public int currentRound = 0;
    public int totalRounds = 3;     //this could be configurable per level
    public int[] roundScores;   // array to keep track of scores for each round

    // add methods to start new round, end the current round, handle the transition
    // between rounds

    // add method to handle end of game after all rounds have been completed

    // on the frame when a script is enabled just before any of the Update methods are called the first time
    private void Start()
    {
        // initiailize roundScores array when the game starts
        roundScores = new int[totalRounds];
    }


    // call this method to start a new round
    public void StartNewRound()
    {
        if (currentRound < totalRounds)
        {
            currentRound++;
            //restart the level for the new round here
            // notify logicscript to reset level state
            //notify uimanager to update the ui for new round
        }
        else
        {
            DisplayEndGameResults();
        }
    }

    // Call this method when a round ends
    public void EndRound(int scoreForRound)
    {
        // update the score for the round that just ended
        roundScores[currentRound - 1] = scoreForRound;
        // display resilts through UIManager
        // Decide wheter to start a new round or end the game
        StartNewRound();
    }



    // Method to display end game results
    private void DisplayEndGameResults()
    {
        // Calculate total score
        int totalScore = 0;
        foreach (int score in roundScores)
        {
            totalScore += score;
        }
        // Notify UIManager to display total score and end game results
        // Provide options to restart the level or return to the level select menu
    }




    //below are pause menu stuff

    private void InstantiatePauseMenu()
    {
        if (pauseMenuPrefab != null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab);
            pauseMenuInstance.SetActive(false);
            DontDestroyOnLoad(pauseMenuInstance); // make sure the pause menu persists across scenes
        }
        else
        {
            Debug.LogWarning("PauseMenuPrefab is not assigned in the inspector.");
        }
    }

    private void InstantiateUIManager()
    {
        if (UIManagerPrefab != null && UIManagerInstance == null)
        {
            UIManagerInstance = Instantiate(UIManagerPrefab);
            DontDestroyOnLoad(UIManagerInstance);

            // after UIManager is instantiated, get the UIManagerScript
            // from the instantiated component
            uiManager = UIManagerInstance.GetComponent<UIManagerScript>();



            if (uiManager == null)
            {
                Debug.LogError("UIManagerInstance does not have a UIManagerScript Component.");
            }
        }
        else
        {
            Debug.Log("UIManagerPrefab is not assigned in the inspector or instance already exists.");
        }
    }

    // (not a unity built in method)
    // called when a scene is loaded, it tells the uiManager to change the ui label
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentStateIndex = 0; // make sure when a scene loads, the first stateis item selection
        UpdateUIManagerStateLabel();
        if (uiManager != null)
        {
            uiManager.UpdateCurrentSceneLabel(scene.name);
        }
    }

    // onEnable and onDisable are built in unity methods automatically called by unity.
    // onenable is called when the scrupt is first loaded. they add and remove event listeners
    // when the 'SceneManager.sceneLoaded' function is called


    // 'SceneManager.sceneLoaded' is a unity built in method that is called when a scene
    // loads. if a function subscribes to it, then it is ALSO called when it is called,
    // like the subscribed functions are attatched to it
    private void OnEnable()
    {
        // subscribe the onscene loaded functino to call when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // unsubscribe
    }




    public void StartGame()
    {
        Debug.Log("Start Game Button Pressed");
        SceneManager.LoadScene("Level-Select");
    }

    public void SettingsButtonPressed()
    {
        Debug.Log("Settings Button pressed");
    }

    public void QuitGame()
    {
        Debug.Log("Game Exiting...");
        Application.Quit();
    }


    

    public void TogglePauseMenu()
    {
        if (pauseMenuInstance != null)
        {
            // Toggle the pause state based on the menu's current active state, not the Time.timeScale
            bool isCurrentlyPaused = pauseMenuInstance.activeSelf;

            // Set the active state of the menu to the opposite of what it currently is
            pauseMenuInstance.SetActive(!isCurrentlyPaused);

            // Pause the game if it is currently unpaused, unpause if it is paused
            Time.timeScale = isCurrentlyPaused ? 1 : 0;

            Debug.Log("Toggling pause menu: " + !isCurrentlyPaused);
        }
        else
        {
            Debug.LogWarning("Pause menu instance not found in the scene!");
        }
    }

    public void GoToMenuScene()
    {
        Debug.Log("Loading Menu Scene");

        // Reset the time scale to normal
        Time.timeScale = 1;

        // If there's a pause menu instance, deactivate it
        if (pauseMenuInstance)
        {
            Destroy(pauseMenuInstance);
            pauseMenuInstance = null;
        }

        // Reset the GameManager instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        // Load the main menu scene
        SceneManager.LoadScene("MainMenuScene");
    }

    // method for loading a new level scene
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
