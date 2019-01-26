using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float startSpeed = 10;
    public float movementSpeed;
    public float MAX_SPEED = 20;
    public float acceleration = 0.1f;
	[SerializeField]
	private int playerNumber;

    public float turningSpeed = 200;
    float time = 0;

    void start() {
        movementSpeed = startSpeed;
    }
    void Update() {
		int playerNum = playerNumber - 1;
        float horizontal = Input.GetAxis("Horizontal" + playerNum.ToString()) * turningSpeed * Time.deltaTime;
        Debug.Log("horz:" + horizontal);
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical" + playerNum.ToString()) * movementSpeed * Time.deltaTime;
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
		
		bool fire = Input.GetButtonDown("Fire" + playerNum.ToString());
    }
   
    void FixedUpdate()
    {
        
    }

}