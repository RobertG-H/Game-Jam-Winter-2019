using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float startSpeed = 10;
    public float movementSpeed;
    public float MAX_SPEED = 20;
    public float acceleration = 0.1f;
	public float turningSpeed = 200;
    public bool dead = false;
	public Rigidbody rb;
    public AudioSource audioSource;
    public AudioSource sfxSource;
    public AudioClip deathSound;
    public AudioClip stepSound;
    public AudioClip dashSound;
    public AudioClip reviveSound;
    public bool steppingSound = false;

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
        rb = GetComponent<Rigidbody> ();
       // AudioSource [] audioSources = GetComponents<AudioSource> ();
        //audioSource = audioSources [0];
       //sfxSource = audioSources [1];


        if (gameObject.tag == "SmallFish") {
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
        if ( vertical > 0.1f ) {
            if ( movementSpeed < MAX_SPEED ) {
                movementSpeed += acceleration;
            }
            transform.Translate(new Vector3(0, 0, vertical));
            anim.SetFloat ("speedPercent", vertical*10f);
            if(!steppingSound) {
                StartCoroutine (steppingSoundEvent ());
            }
        }
        else if (vertical < -0.1f) {
            transform.Translate(new Vector3(0,0, vertical));
            anim.SetFloat ("speedPercent", vertical*10f);
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
        anim.SetBool ("dead", deathController.isDead);
        if(deathController.isDead && !dead) {
            dead = true;
            audioSource.volume = 0;
            sfxSource.volume = 0.5f;
            sfxSource.clip = deathSound;
            sfxSource.Play();
        }
        else if (!deathController.isDead && dead) {
            dead = false;
            audioSource.volume = 0.65f;
            sfxSource.volume = 0.5f;
            sfxSource.clip = reviveSound;
            sfxSource.Play ();
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
        Debug.Log ("DASHING");
        sfxSource.clip = dashSound;
        sfxSource.Play ();
        sfxSource.volume = 0.5f;
        anim.SetBool ("roll", true);
        Dash = transform.forward * 30; //fix this later
        rb.AddForce(Dash, ForceMode.Impulse);
		waitActive = true;
		yield return new WaitForSeconds(dashCooldown / 2);
        Debug.Log ("Animation dash complete");
        anim.SetBool ("roll", false);
        yield return new WaitForSeconds (dashCooldown / 2);
        Debug.Log ("Dash ready");
        waitActive = false;
    }

    IEnumerator steppingSoundEvent () {
        steppingSound = true;
        Debug.Log ("Stepping");
        sfxSource.clip = stepSound;
        sfxSource.volume = 0.3f;
        sfxSource.Play ();

        yield return new WaitForSeconds (0.3f);
        steppingSound = false;
    }
}

