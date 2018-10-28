using UnityEngine;
using System.Collections;

public class ObstacleControllerRotation : MonoBehaviour
{
    public float velocity = 30f;

    // 1 = move right, 0 = don't move, -1 = move left (relative to rotation)
    private int direction;

    private float currentRotation;

    // Use this for initialization
    void Start()
    {
        // rotation is randomly generated for each obstacle
        currentRotation = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        // generate a random integer-number from -1 to 1
        direction = Random.Range(-1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the superior gameObject and with it all cubes
        currentRotation += Time.deltaTime * velocity * direction;
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
