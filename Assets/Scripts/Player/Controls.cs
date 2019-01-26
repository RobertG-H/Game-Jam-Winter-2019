using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float movementSpeed = 30;
    public float acceleration = 10;
    public float turningSpeed = 200;
    float time = 0;
    public Rigidbody rb;

    void start() {
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
        
        if ( Input.GetKey(KeyCode.W) ) {
            
            Debug.Log("velocity: " + rb.velocity.magnitude);
            float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * time;
            transform.Translate(0, 0, vertical);
            if ( rb.velocity.magnitude < 1f/1000000000000000000000000f ) {
                time += Time.deltaTime * acceleration;
            }
        } else if (Input.GetKeyUp(KeyCode.W)) {
            time = 0;
        }
        
    }   
    void FixedUpdate()
    {
        
    }
}