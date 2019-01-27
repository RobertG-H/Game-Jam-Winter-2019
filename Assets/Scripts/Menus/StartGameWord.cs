using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWord : MonoBehaviour
{
    private float yMax;
    private float yMin;
    public float range;
    public float ySpeed;
    private bool movingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        yMax = transform.localPosition.y + range;
        yMin = transform.localPosition.y - range;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > yMax) {
            movingUp = false;
        }
        else if (transform.localPosition.y < yMin) {
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
