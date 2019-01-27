using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // requires you to set up axes "Joy0X" - "Joy3X" and "Joy0Y" - "Joy3Y" in the Input Manger
        for (int i = 0; i < 4; i++) {
            if (Mathf.Abs (Input.GetAxis ("Vertical" + i)) > 0.2) {
                Debug.Log (Input.GetJoystickNames () [i] + " is moved");
            }
        }
    }
}
