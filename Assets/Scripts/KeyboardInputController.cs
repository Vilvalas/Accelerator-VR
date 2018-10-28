using UnityEngine;
using System.Collections;

public class KeyboardInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            // pressing any key resets the Head-Tracking rotation
            OVRManager.display.RecenterPose();
        }
    }
}
