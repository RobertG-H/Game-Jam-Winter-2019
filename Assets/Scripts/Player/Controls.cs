using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float startSpeed = 10;
    public float movementSpeed;
    public float MAX_SPEED = 20;
    public float acceleration = 0.1f;
	public float turningSpeed = 200;
	public Rigidbody rb;
	public int playerNumber;
	public float impulsePower = 15;

    private bool grounded = true;

    private Vector3 Dash;
    private bool blocked = false;
    private bool waitActive = false;
    void Start() {
        movementSpeed = startSpeed;
    }
    void Update() {
		int playerNum = (int)playerNumber - 1;
        float horizontal = Input.GetAxis("Horizontal" + playerNum.ToString()) * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical" + playerNum.ToString()) * movementSpeed * Time.deltaTime;
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

        if ( Input.GetKeyDown(KeyCode.V) && grounded ) {
            if ( rb.velocity.x == 0 )
                rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
            else {
                rb.AddForce(new Vector3(0,12,1), ForceMode.Impulse);
            }
        }

        bool fire = Input.GetButtonDown("Fire" + playerNum.ToString());
		if (fire && playerNum == 0) { // should be !=, its 0 for testing
			if (!waitActive) {
				StartCoroutine(Wait());
			}
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
	
	IEnumerator Wait()
	{
        Dash = transform.forward;
        Dash = impulsePower * Dash;
        rb.AddForce(Dash, ForceMode.Impulse);
		waitActive = true;
		yield return new WaitForSeconds(1.5f);
		waitActive = false;

	}
}

