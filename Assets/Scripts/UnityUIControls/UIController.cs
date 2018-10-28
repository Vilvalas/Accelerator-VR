using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameController gameController;
    public Button startButton;
    public Button readyButton;

    // quality setting buttons
    public Button lowButton;
    public Button medButton;
    public Button highButton;

    // this button is on the gameOver canvas
    public Button restartButton;

    public Text mainMenuHighScoreText;
    public Text gameOverHighScoreText;
    public Text personalScoreText;

    public float buttonActivationTime = 1f;

    private enum buttonType { start, ready, restart, low, med, high };
    private buttonType currentButtonType;

    // indicates for how long the currently selected button is selected
    private float focusTimer;

    // indicates if a button is currently selected
    private bool buttonSelected;

    private bool readyPressed;

    private Button lastQualityButtonPressed;

    // Use this for initialization
    void Start () {
        // disable startButton until ready is pressed
        startButton.interactable = false;

        // set the quality settings to the ones the user chose last time
        // 0 is the default value, which means nothing is saved
        switch (PlayerPrefs.GetInt("qualityLevel"))
        {
            case 1:
                lastQualityButtonPressed = lowButton;
                setQualityLevel("low");
                break;
            case 2:
                lastQualityButtonPressed = medButton;
                setQualityLevel("med");
                break;
            default:
                // game starts automaticly with high quality settings
                lastQualityButtonPressed = highButton;
                break;
        }
        
        lastQualityButtonPressed.interactable = false;

        buttonSelected = false;
        readyPressed = false;
        focusTimer = 0;

        showHighscores(mainMenuHighScoreText);
	}
	
	// Update is called once per frame
	void Update () {
        if(buttonSelected)
        {
            focusTimer += Time.deltaTime;

            if (focusTimer >= buttonActivationTime)
            {
                buttonSelected = false;
                focusTimer = 0;
                launchButtonAction();
            }
        }
    }

    // resets this script
    public void Reset()
    {
        Start();
    }

    public void PrepareGameOverUI(int playerScore)
    {
        personalScoreText.text = "Your Score: " + playerScore;

        if (playerScore > PlayerPrefs.GetInt("highscore1"))
        {
            PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4"));
            PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3"));
            PlayerPrefs.SetInt("highscore3", PlayerPrefs.GetInt("highscore2"));
            PlayerPrefs.SetInt("highscore2", PlayerPrefs.GetInt("highscore1"));
            PlayerPrefs.SetInt("highscore1", playerScore);
        } else if (playerScore > PlayerPrefs.GetInt("highscore2"))
        {
            PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4"));
            PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3"));
            PlayerPrefs.SetInt("highscore3", PlayerPrefs.GetInt("highscore2"));
            PlayerPrefs.SetInt("highscore2", playerScore);
        } else if (playerScore > PlayerPrefs.GetInt("highscore3"))
        {
            PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4"));
            PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3"));
            PlayerPrefs.SetInt("highscore3", playerScore);
        } else if (playerScore > PlayerPrefs.GetInt("highscore4"))
        {
            PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4"));
            PlayerPrefs.SetInt("highscore4", playerScore);
        } else if (playerScore > PlayerPrefs.GetInt("highscore5"))
        {
            PlayerPrefs.SetInt("highscore5", playerScore);
        }

        showHighscores(gameOverHighScoreText);
    }

    private void launchButtonAction()
    {
        switch (currentButtonType)
        {
            case buttonType.ready:
                readyButton.interactable = false;
                readyPressed = true;
                startButton.interactable = true;
                break;
            case buttonType.start:
                if (readyPressed)
                {
                    readyButton.interactable = true;
                    gameController.StartGame();
                }
                break;
            case buttonType.low:
                if (lastQualityButtonPressed != lowButton)
                {
                    setQualityLevel("low");
                    lastQualityButtonPressed.interactable = true;
                    lowButton.interactable = false;
                    lastQualityButtonPressed = lowButton;
                    PlayerPrefs.SetInt("qualityLevel", 1);
                }
                break;
            case buttonType.med:
                if (lastQualityButtonPressed != medButton)
                {
                    setQualityLevel("med");
                    lastQualityButtonPressed.interactable = true;
                    medButton.interactable = false;
                    lastQualityButtonPressed = medButton;
                    PlayerPrefs.SetInt("qualityLevel", 2);
                }
                break;
            case buttonType.high:
                if (lastQualityButtonPressed != highButton)
                {
                    setQualityLevel("high");
                    lastQualityButtonPressed.interactable = true;
                    highButton.interactable = false;
                    lastQualityButtonPressed = highButton;
                    PlayerPrefs.SetInt("qualityLevel", 3);
                }
                break;
            case buttonType.restart:
                gameController.RestartGame();
                break;
        }
    }

    private void setQualityLevel(string qualityLevel)
    {
        switch (qualityLevel)
        {
            case "low":
                QualitySettings.SetQualityLevel(1);
                break;
            case "med":
                QualitySettings.SetQualityLevel(3);
                break;
            case "high":
                QualitySettings.SetQualityLevel(5);
                break;
            default:
                Debug.Log("Unknown QualityLevel");
                break;
        }
    }

    private void showHighscores(Text textfield)
    {
        if (PlayerPrefs.GetInt("highscore1") == 0)
        {
            // set default values
            PlayerPrefs.SetInt("highscore1", 50000);
            PlayerPrefs.SetInt("highscore2", 40000);
            PlayerPrefs.SetInt("highscore3", 30000);
            PlayerPrefs.SetInt("highscore4", 20000);
            PlayerPrefs.SetInt("highscore5", 10000);
        }
        textfield.text = PlayerPrefs.GetInt("highscore1") + "\n"
            + PlayerPrefs.GetInt("highscore2") + "\n"
            + PlayerPrefs.GetInt("highscore3") + "\n"
            + PlayerPrefs.GetInt("highscore4") + "\n"
            + PlayerPrefs.GetInt("highscore5");
    }

    public void ReadyButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.ready;
    }

    public void StartButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.start;
    }

    public void LowButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.low;
    }

    public void MedButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.med;
    }

    public void HighButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.high;
    }

    public void RestartButtonSelected()
    {
        buttonSelected = true;
        currentButtonType = buttonType.restart;
    }

    public void ButtonDeselected()
    {
        buttonSelected = false;
        focusTimer = 0;
    }

}
