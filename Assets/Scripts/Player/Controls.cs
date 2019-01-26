using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float startSpeed = 10;
    public float movementSpeed;
    public float MAX_SPEED = 20;
    public float acceleration = 0.1f;

    public float turningSpeed = 200;

    private bool grounded = true;
    float time = 0;
    public Rigidbody rb;
    void start() {
        movementSpeed = startSpeed;
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        float horizontal = Input.GetAxis("Horizontal0") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Vertical0") * movementSpeed * Time.deltaTime;
        if ( Input.GetKey(KeyCode.W) ) {
            if ( movementSpeed < MAX_SPEED ) {
                movementSpeed += acceleration;
            }
            transform.Translate(new Vector3(0, 0, vertical)); 
        } else if ( Input.GetKey(KeyCode.S) ) {
            transform.Translate(new Vector3(0,0, vertical));
        }
        if ( Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ){
            movementSpeed = startSpeed;
        }

        if ( Input.GetKeyDown(KeyCode.Space) && grounded ) {
            if ( rb.velocity.x == 0 )
                rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
            else {
                rb.AddForce(new Vector3(0,12,1), ForceMode.Impulse);
            }
                

        } else if ( !grounded ) {

        }
        
    }   
    void OnCollisionExit(Collision col ) {
        if ( col.gameObject.tag == "Land" ) {
            grounded = false;
        }
    }
    void OnCollisionStay(Collision col){
        if ( col.gameObject.tag == "Land" ) {
            grounded = true;
        }
    }
    void FixedUpdate()
    {
        
    }

}