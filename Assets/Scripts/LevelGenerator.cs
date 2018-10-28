using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public GameObject segment1Prefab;
    public GameObject segment2Prefab;
    public GameObject segment3Prefab;
    public GameObject segment4Prefab;
    public GameObject segment5Prefab;

    // indicate how many prefabs are used in the generation
    public int numberOfPrefabs = 5;

    public int numberOfObstacles = 100;
    public float startingPosition = 0f;
    public float spawnGap = 30f;

    private GameObject spawnedObject;
    private bool obstacle5Flag = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void generateLevel()
    {
        int currentPrefab;
        float currentZPosition = startingPosition;

        for (int i = 0; i < numberOfObstacles; i++)
        {
            // make sure the fifth Obstacle doesn't occur twice in a row
            if (obstacle5Flag)
            {
                currentPrefab = Random.Range(1, numberOfPrefabs);
                obstacle5Flag = false;
            }
            else
            {
                // generate a random obstacle
                currentPrefab = Random.Range(1, numberOfPrefabs + 1);
            }

            switch (currentPrefab)
            {
                case 1:
                    spawnedObject = (GameObject) Object.Instantiate(segment1Prefab, new Vector3(0, 0, currentZPosition), new Quaternion());
                    break;
                case 2:
                    spawnedObject = (GameObject) Object.Instantiate(segment2Prefab, new Vector3(0, 0, currentZPosition), new Quaternion());
                    break;
                case 3:
                    spawnedObject = (GameObject) Object.Instantiate(segment3Prefab, new Vector3(0, 0, currentZPosition), new Quaternion());
                    break;
                case 4:
                    spawnedObject = (GameObject) Object.Instantiate(segment4Prefab, new Vector3(0, 0, currentZPosition), new Quaternion());
                    break;
                case 5:
                    spawnedObject = (GameObject) Object.Instantiate(segment5Prefab, new Vector3(0, 0, currentZPosition), new Quaternion());
                    obstacle5Flag = true;
                    break;
            }
            // LevelGenerator is located on the main geometry object
            spawnedObject.transform.SetParent(transform);
            currentZPosition += spawnGap;
        }
    }
}
