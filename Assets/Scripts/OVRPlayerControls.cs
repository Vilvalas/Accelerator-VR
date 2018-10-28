using UnityEngine;
using System.Collections;

public class OVRPlayerControls : MonoBehaviour {

    public GameObject centerEyeAnchor;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = centerEyeAnchor.transform.localRotation;
    }
}
