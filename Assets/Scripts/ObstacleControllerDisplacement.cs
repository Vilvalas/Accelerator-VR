using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameObject))]
public class ObstacleControllerDisplacement : MonoBehaviour {

    public GameObject cube;
    public float maxDistance = 1.75f;

	// Use this for initialization
	void Start () {
        // position of the hole is randomly generated for each obstacle
        float positionX = Random.Range(-maxDistance, maxDistance);
        float positionY = Random.Range(-maxDistance, maxDistance);

        // since the cube is rotated we have to use Vector3.forward
        cube.transform.Translate(Vector3.right * positionX);
        cube.transform.Translate(Vector3.forward * positionY);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
