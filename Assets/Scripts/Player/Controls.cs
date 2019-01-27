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
	public float dashCooldown = 1.5f;

    private bool grounded = true;
	public bool biting = false;

    private Vector3 Dash;
    private bool blocked = false;
    private bool waitActive = false;

    private DeathController deathController;

    private Animator anim;
    void Start() {
        movementSpeed = startSpeed;
        anim = GetComponent<Animator>();

        if(gameObject.tag == "SmallFish") {
            deathController = GetComponent<DeathController> ();
        }
    }
    void Update() {

        float horizontal = Input.GetAxis("Horizontal" + playerNumber.ToString()) * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        if (!canMove()) {
            return;
        }

        float vertical = Input.GetAxis("Vertical" + playerNumber.ToString()) * movementSpeed * Time.deltaTime;

        if ( vertical>0.1f ) {
            if ( movementSpeed < MAX_SPEED ) {
                movementSpeed += acceleration;
            }
            transform.Translate(new Vector3(0, 0, vertical));
            anim.SetFloat ("speedPercent", 10f);
        }
        else if (vertical < -0.1f) {
            transform.Translate(new Vector3(0,0, vertical));
            anim.SetFloat ("speedPercent", 10f);
        }
        else {
            anim.SetFloat ("speedPercent", 0f);
            movementSpeed = startSpeed;
        }

        if ( Input.GetKeyDown(KeyCode.V) && grounded ) {
            if ( rb.velocity.x == 0 )
                StartCoroutine(jump());
            else {
                rb.AddForce(new Vector3(0,12,1), ForceMode.Impulse);
            }
        }

        //fix this later
        bool fire = Input.GetButtonDown("Fire" + playerNumber.ToString());
		if (fire) {
			if (!waitActive) {
				if (playerNumber != 0) {
					StartCoroutine(DashEvent());
				}
			}
		}
        
    }   

    bool canMove() {
        if(gameObject.tag != "SmallFish") {
            return true;
        }
        return !deathController.isDead;
    }

    void OnCollisionExit(Collision col ) {
        if ( col.gameObject.tag == "Land" ) {
            grounded = false;
        }
    }
    void OnCollisionStay(Collision col){
        if ( col.gameObject.tag == "Land" ) {
            StartCoroutine(land());
        }
    }
	
    IEnumerator jump() {
        anim.SetBool("jump", true);
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
    }

    IEnumerator land(){
        anim.SetBool("jump", false);
        grounded = true;
        yield return new WaitForSeconds(0.0f);
    }

    IEnumerator DashEvent()
	{
        Dash = transform.forward * 25; //fix this later
        rb.AddForce(Dash, ForceMode.Impulse);
		waitActive = true;
		yield return new WaitForSeconds(dashCooldown);
		waitActive = false;

	}



}

