using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    float x_offset;
    float z_offset;
    // Start is called before the first frame update
    void Start()
    {
        x_offset = target.transform.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        x_offset = target.transform.position.x - (rotation * new Vector3(x_offset,0,0)).x;
        z_offset = target.transform.position.z - (rotation * new Vector3(0,0,z_offset)).z;
        Vector3 camera_delta = new Vector3(x_offset, transform.position.y, z_offset);

        Debug.Log(camera_delta);
        Debug.Log();

        transform.position = camera_delta;
        //transform.position = new Vector3(target.transform.position.x - (rotation.x * x_offset), transform.position.y, 0);
        transform.LookAt(target.transform);
    }
}
