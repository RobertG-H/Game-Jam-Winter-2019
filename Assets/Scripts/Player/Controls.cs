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
	public float impulsePower = 6;

	private Vector3 Dash;
	private bool blocked = false;
	private bool waitActive = false;
    float time = 0;

    void Start() {
        movementSpeed = startSpeed;
    }
    void Update() {
		int playerNum = playerNumber - 1;
        float horizontal = Input.GetAxis("Horizontal" + playerNum.ToString()) * turningSpeed * Time.deltaTime;
        //Debug.Log("horz:" + horizontal);
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical" + playerNum.ToString()) * movementSpeed * Time.deltaTime;
        if ( Input.GetKey(KeyCode.W) ) {
            if ( movementSpeed < MAX_SPEED ) {
                movementSpeed += acceleration;
                //Debug.Log("speed: " + movementSpeed);
            }
            transform.Translate(new Vector3(0, 0, vertical)); 
        } else if ( Input.GetKey(KeyCode.S) ) {
            transform.Translate(new Vector3(0,0, vertical));
        }
        if ( Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ){
            movementSpeed = startSpeed;
        }
		
		bool fire = Input.GetButtonDown("Fire" + playerNum.ToString());
		if (fire && playerNum == 0) { // should be !=, its 0 for testing
			if (!waitActive) {
				StartCoroutine(Wait());
			}
			if (!blocked) {
				Dash = transform.forward;
				Dash = impulsePower * Dash;
				rb.AddForce(Dash, ForceMode.Impulse);
			}
		}
    }
	
	IEnumerator Wait()
	{
		waitActive = true;
		yield return new WaitForSeconds(1.5f);
		blocked = false;
		waitActive = false;

	}

    void FixedUpdate()
    {
        
    }

}