using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GameObject))]
public class PlayerController : MonoBehaviour {

    public GameObject gameOverSpherePrefab;
    public GameObject geometryObjects;
    public GameController gameController;
    public float startingVelocity = 2f;
    public float acceleration = 0.25f;
    public float backtrackingVelocity = 2f;
    public float backtrackingDistance = 4f;

    private enum playerState { moving, backtracking, inactive };
    private playerState currentState;

    private float currentVelocity;
    private float backtrackedDistance;
    private Vector3 backtrackingVector;
    private GameObject spawnedSphere;

    private int playerScore;

    // Use this for initialization
    void Start () {
        currentVelocity = startingVelocity;
        backtrackedDistance = 0;
        currentState = playerState.inactive;
    }

    // this gets called when the player starts the game from the UI
    public void StartMoving()
    {
        currentState = playerState.moving;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState == playerState.moving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * currentVelocity);
            currentVelocity += acceleration * Time.deltaTime;
        } else if (currentState == playerState.backtracking)
        {
            // indicates how far the player will move in this update
            float stepDistance = Time.deltaTime * backtrackingVelocity;

            transform.Translate(backtrackingVector * stepDistance);
            backtrackedDistance += stepDistance;

            if (backtrackedDistance >= backtrackingDistance)
            {
                currentState = playerState.inactive;

                gameController.EndGame(playerScore);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == playerState.moving)
        {
            // create a gameOverSphere at the current player position to indicate where he collided
            spawnedSphere = (GameObject) Object.Instantiate(gameOverSpherePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());

            spawnedSphere.transform.SetParent(geometryObjects.transform);

            // save the current camera orientation for backtracking
            backtrackingVector = Vector3.back;

            // calculate the player score
            playerScore = (int) transform.position.z * 100;

            currentState = playerState.backtracking;
        }
    }

    public void Reset()
    {
        Start();
        transform.position = new Vector3(0, 0, 0);
    }
}
