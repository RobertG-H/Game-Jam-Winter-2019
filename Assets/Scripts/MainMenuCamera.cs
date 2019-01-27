using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{

    public GameObject target;//the target object
    public float speedMod = 10.0f;//a speed modifier
    private Vector3 point;//the coord to the point where the camera looks at
    public float yMax;
    public float yMin;
    public float ySpeed;
    private bool movingUp = false;

    void Start()
    {
        point = target.transform.position;//get target's coords
        transform.LookAt (point);//makes the camera look to it
    }

    void Update()
    {
        transform.RotateAround (point, new Vector3 (0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        if (transform.position.y > yMax) {
            movingUp = false;
        } else if (transform.position.y < yMin) {
            movingUp = true;
        }
        if (movingUp) {
            transform.position = (new Vector3 (transform.position.x, transform.position.y + ySpeed, transform.position.z));
        } else {
            transform.position = (new Vector3 (transform.position.x, transform.position.y - ySpeed, transform.position.z));
        }
    }
}
