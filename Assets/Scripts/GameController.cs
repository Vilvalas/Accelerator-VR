using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public LevelGenerator levelGenerator;
    public PlayerController playerController;
    public UIController uiController;
    public ScreenFadeController screenFadeController;
    public GameObject mainMenuGUI;
    public GameObject gameOverGUI;
    public GameObject focusTracker;
    public GameObject geometryObjects;

    private enum gameState { ending, resetting };
    private gameState currentState;

    private int playerScore;

    // Use this for initialization
    void Start () {
        levelGenerator.generateLevel();
        screenFadeController.FadeBlackToAlpha();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // this gets called when the player starts the game from the UI
    public void StartGame()
    {
        mainMenuGUI.SetActive(false);
        focusTracker.SetActive(false);
        playerController.StartMoving();
    }

    public void EndGame(int score)
    {
        playerScore = score;

        // slowly fade the camera to black
        screenFadeController.FadeAlphaToBlack();

        currentState = gameState.ending;
    }

    public void RestartGame()
    {
        // slowly fade the camera to black
        screenFadeController.FadeAlphaToBlack();

        currentState = gameState.resetting;
    }

    // this gets called by screenFader when the screen is completely converted to black
    public void ScreenCompletelyBlack()
    {
        switch (currentState)
        {
            case gameState.ending:

                // deactivate all geometry objects
                geometryObjects.SetActive(false);

                // show gameOverGUI
                gameOverGUI.SetActive(true);
                focusTracker.SetActive(true);

                uiController.PrepareGameOverUI(playerScore);

                playerController.Reset();

                // slowly fade the camera back to normal
                screenFadeController.FadeBlackToAlpha();

                break;
            case gameState.resetting:

                // reload the scene, resulting in destroying all obstacles and generating new ones
                // this leads to a performance overhead and should therefore be avoided
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                // instead we recycle our objects as far as possible

                // reset the GameObjects to the starting state
                geometryObjects.SetActive(true);
                gameOverGUI.SetActive(false);
                mainMenuGUI.SetActive(true);
                uiController.Reset();

                // destroy the GameObjects we don't need anymore
                GameObject[] gameObjectsToRemove = GameObject.FindGameObjectsWithTag("Prefab");

                for (var i = 0; i < gameObjectsToRemove.Length; i++)
                {
                    Destroy(gameObjectsToRemove[i]);
                }

                // make the scene visible again
                screenFadeController.FadeBlackToAlpha();

                // reset the GameController itself
                Start();

                break;
        }
    }
    
}
