using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    float x_offset;
    float z_offset;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        x_offset = target.transform.position.x - transform.position.x;
        z_offset = target.transform.position.z - transform.position.z;
        offset   = new Vector3(x_offset, transform.position.y, z_offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        Vector3 camera_delta = target.transform.position - (rotation * offset);
        camera_delta = new Vector3(camera_delta.x, transform.position.y, camera_delta.z);

        transform.position = camera_delta;
        transform.LookAt(target.transform);
    }
}
