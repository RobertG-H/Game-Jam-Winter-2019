using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float startSpeed = 10;
    public float movementSpeed;
    public float MAX_SPEED = 20;
    public float acceleration = 0.1f;

    public float turningSpeed = 200;
    float time = 0;
    public Rigidbody rb;

    void start() {
        rb = GetComponent<Rigidbody>();
        movementSpeed = startSpeed;
    }
    void Update() {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        if ( Input.GetKey(KeyCode.W) ) {
            if ( movementSpeed < MAX_SPEED ) {
                movementSpeed += acceleration;
                Debug.Log("speed: " + movementSpeed);
            }
            transform.Translate(new Vector3(0, 0, vertical)); 
        } else if ( Input.GetKey(KeyCode.S) ) {
            transform.Translate(new Vector3(0,0, vertical));
        }
        if ( Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ){
            movementSpeed = startSpeed;
        }
        
    }   
    void FixedUpdate()
    {
        
    }

}