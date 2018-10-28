using UnityEngine;
using System.Collections;

public class ScreenFadeController : MonoBehaviour
{
    public GameController gameController;

    // Speed that the screen fades to and from black
    public float fadeSpeed = 0.5f;

    public float fadeBoundary = 0.1f;

    private GUITexture myGUITexture;

    // take the DK 2 display specs (each eye separated) for screen dimensions
    private int screenWidth = 960;
    private int screenHeight = 1080;

    private enum screenFaderState { blackToAlpha, alphaToBlack, idle };
    private screenFaderState currentState;

    // Awake is called before any Start Methods
    void Awake()
    {
        myGUITexture = GetComponent<GUITexture>();

        // Set the texture so that it is the the size of the screen and covers it
        // note: this returns 955 x 485 for the DK 2, which is not the real display size
        // myGUITexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

        myGUITexture.pixelInset = new Rect(0f, 0f, screenWidth, screenHeight);

        currentState = screenFaderState.idle;
    }

    void Update()
    {
        switch(currentState)
        {
            case screenFaderState.idle:
                return;

            case screenFaderState.blackToAlpha:
                FadeToClear();
                if (myGUITexture.color.a <= fadeBoundary)
                {
                    // When the screen is almost clear, set the colour to clear and disable the GUITexture.
                    myGUITexture.color = Color.clear;
                    myGUITexture.enabled = false;
                    currentState = screenFaderState.idle;
                }
                return;

            case screenFaderState.alphaToBlack:
                FadeToBlack();
                if (myGUITexture.color.a >= (1- fadeBoundary))
                {
                    // When the screen is almost black
                    myGUITexture.color = Color.black;
                    myGUITexture.enabled = false;
                    currentState = screenFaderState.idle;
                    gameController.ScreenCompletelyBlack();
                }
                return;
        }
    }

    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        myGUITexture.color = Color.Lerp(myGUITexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        myGUITexture.color = Color.Lerp(myGUITexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    public void FadeBlackToAlpha()
    {
        // Make sure the texture is enabled.
        myGUITexture.enabled = true;

        currentState = screenFaderState.blackToAlpha;
    }


    public void FadeAlphaToBlack()
    {
        // Make sure the texture is enabled.
        myGUITexture.enabled = true;

        currentState = screenFaderState.alphaToBlack;
    }
}