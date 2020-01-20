using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 1;
    Vector3 offset;
    public int playerNumber;

    private float horizontal = 0;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        // float horizontal = Input.GetAxis("Camera" + playerNumber.ToString()) * rotateSpeed;
        // //Debug.Log (playerNumber + ": " + Input.GetAxis ("Camera" + playerNumber.ToString ()));
        // if (Mathf.Abs(Input.GetAxis("Camera" + playerNumber.ToString())) > 0.1f)
        // {
        //     target.transform.Rotate(0, horizontal, 0);
        // }
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            Debug.Log("MOVING CAMERA");
            target.transform.Rotate(0, horizontal, 0);
        }
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);

    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        Debug.Log("On Camera");
        horizontal = context.ReadValue<float>() * rotateSpeed;
        Debug.Log(horizontal);
    }
}