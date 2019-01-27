using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWord : MonoBehaviour
{
    public float yMax;
    public float yMin;
    public float ySpeed;
    private bool movingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log (transform.position.y);
        if (transform.position.y > yMax) {
            movingUp = false;
        }
        else if (transform.position.y < yMin) {
            movingUp = true;
        }
        if (movingUp) {
            transform.position = (new Vector3 (transform.position.x, transform.position.y + ySpeed, transform.position.z));
        }
        else {
            transform.position = (new Vector3 (transform.position.x, transform.position.y - ySpeed, transform.position.z));
        }
    }
}
