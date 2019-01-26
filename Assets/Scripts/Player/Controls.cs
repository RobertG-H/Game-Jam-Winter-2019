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
	public float biteCooldown = 1.0f;
	public float dashCooldown = 1.5f;

    private bool grounded = true;
	private bool biting = false;

    private Vector3 Dash;
	private Vector3 Bite;
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
		if (fire) {
			if (!waitActive) {
				if (playerNum != 0) {
					StartCoroutine(DashEvent());
				}
				else {
					StartCoroutine(BiteEvent());
				}
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
	
	void OnCollisionEnter(Collision col) {
		if ( col.gameObject.tag == "SmallFish" && biting) {
			biting = false; // death event
		}
	}
	
	IEnumerator DashEvent()
	{
        Dash = transform.forward;
        Dash = impulsePower * Dash;
        rb.AddForce(Dash, ForceMode.Impulse);
		waitActive = true;
		yield return new WaitForSeconds(dashCooldown);
		waitActive = false;

	}
	
	IEnumerator BiteEvent()
	{
        Bite = transform.forward;
        Bite = impulsePower * Bite;
        rb.AddForce(Bite, ForceMode.Impulse);
		waitActive = true;
		biting = true;
		movementSpeed = 0;
		yield return new WaitForSeconds(biteCooldown);
		waitActive = false;
		biting = false;
		movementSpeed = startSpeed;
	}
}

