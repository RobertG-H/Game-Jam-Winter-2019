using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 1;
    Vector3 offset;
     
    void Start() {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate() {
        float horizontal = Input.GetAxis("Camera0") * rotateSpeed;
        if ( Mathf.Abs(Input.GetAxis("Camera0")) > 0.1f ) {
            target.transform.Rotate(0, horizontal, 0);
        }
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
        
    }
}