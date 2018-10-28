using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameObject))]
public class ObstacleControllerMovement : MonoBehaviour {
    public GameObject cube1;
    public GameObject cube2;
    public float velocity = 1;
    public float maxDistance = 4.25f;

    // 1 = move right, 0 = don't move, -1 = move left (relative to rotation)
    private int direction;

    // the distance the cubes have moved away from their starting position
    private float movedDistance;

    private float leftBoundary;
    private float rightBoundary;
    
    // Use this for initialization
    void Start () {
        // rotation is randomly generated for each obstacle
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        // generate a random integer-number from -1 to 1
        direction = Random.Range(-1, 2);

        movedDistance = 0;
        leftBoundary = -maxDistance;
        rightBoundary = maxDistance;
    }

    // Update is called once per frame
    void Update () {
        // indicates how far the cubes will move in this update
        float stepDistance = Time.deltaTime * velocity * direction;

        cube1.transform.Translate(Vector3.right * stepDistance);
        cube2.transform.Translate(Vector3.right * stepDistance);

        movedDistance += stepDistance;

        if (direction == 1)
        {
            if (movedDistance >= rightBoundary)
            {
                direction = -1;
            }
        } else {
            if (movedDistance <= leftBoundary)
            {
                direction = 1;
            }
        }
    }
}
