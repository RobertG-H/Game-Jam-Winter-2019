using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkControls : MonoBehaviour
{

    public float startSpeed;
    public float movementSpeed;
    public float MAX_SPEED;
    public float acceleration;
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

    public AudioSource sfxSource;
    public AudioClip deathSound;
    public AudioClip stepSound;
    public AudioClip biteSound;
    public AudioClip burpSound;
    public bool steppingSound = false;


    Collider col;
    public float biteCooldown = 2.0f;

    public float bitePower = 50;
    public bool canBite = true;
    void Start () {
        movementSpeed = startSpeed;
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody> ();

        if (gameObject.tag == "SmallFish") {
            deathController = GetComponent<DeathController> ();
        }
        col = GetComponent<CapsuleCollider> ();
    }
    void Update () {



        float horizontal = Input.GetAxis ("Horizontal" + playerNumber.ToString ()) * turningSpeed * Time.deltaTime;
        transform.Rotate (0, horizontal, 0);
        transform.GetChild(2).gameObject.transform.Rotate(0, -horizontal, 0);

        if (!canBite) {
            return;
        }

        if (!canMove ()) {
            return;
        }

        float vertical = Input.GetAxis ("Vertical" + playerNumber.ToString ()) * movementSpeed * Time.deltaTime;
        if (vertical > 0.1f) {
            if (movementSpeed < MAX_SPEED) {
                movementSpeed += acceleration;
            }
            transform.Translate (new Vector3 (0, 0, vertical));
            anim.SetFloat ("speedPercent", movementSpeed);
            if (!steppingSound) {
                StartCoroutine (steppingSoundEvent ());
            }
        }
        else if (vertical < -0.1f) {
            transform.Translate (new Vector3 (0, 0, vertical));
            anim.SetFloat ("speedPercent", movementSpeed);
        }
        else {
            anim.SetFloat ("speedPercent", 0f);
            movementSpeed = startSpeed;
        }

        if (Input.GetKeyDown (KeyCode.V) && grounded) {
            if (rb.velocity.x == 0)
                StartCoroutine (jump ());
            else {
                rb.AddForce (new Vector3 (0, 12, 1), ForceMode.Impulse);
            }
        }
        bool fire = Input.GetButtonDown ("Fire" + playerNumber.ToString ());
        if (fire && canBite) {
            Debug.Log ("Biting");
            StartCoroutine (bite ());
        }
    }

    bool canMove () {
        if (gameObject.tag != "SmallFish") {
            return true;
        }
        return !deathController.isDead;
    }

    void OnCollisionExit (Collision col) {
        if (col.gameObject.tag == "Land") {
            grounded = false;
        }
    }

    void OnCollisionStay (Collision col) {
        if (col.gameObject.tag == "Land") {
            StartCoroutine (land ());
        }
    }

    void OnCollisionEnter(Collision collision) {
		if ( collision.gameObject.tag == "SmallFish" && !canBite ) {
            // death event
            collision.gameObject.GetComponent<DeathController> ().die ();
            sfxSource.clip = burpSound;
            sfxSource.pitch = 1f;
            sfxSource.volume = 0.4f;
            sfxSource.PlayDelayed (0.7f);
            Debug.Log("kilt him");
		}
	}
    IEnumerator bite(){
        sfxSource.clip = biteSound;
        sfxSource.pitch = 1f;
        sfxSource.volume = 0.2f;
        sfxSource.Play ();
        anim.SetBool("bite", true);
        canBite = false;
        Debug.Log("false canbite");
        Vector3 Bite = transform.forward * bitePower;
        rb.AddForce(Bite, ForceMode.Impulse);
		yield return new WaitForSeconds(biteCooldown / 2);
        anim.SetBool ("bite", false);
        yield return new WaitForSeconds (biteCooldown / 2);
        canBite = true;
        Debug.Log("true canbite");
        movementSpeed = startSpeed;
    }

    IEnumerator jump () {
        //anim.SetBool ("jump", true);
        yield return new WaitForSeconds (0.5f);
        rb.AddForce (new Vector3 (0, 10, 0), ForceMode.Impulse);
    }

    IEnumerator land () {
        //anim.SetBool ("jump", false);
        grounded = true;
        yield return new WaitForSeconds (0.0f);
    }

    IEnumerator steppingSoundEvent () {
        steppingSound = true;
        Debug.Log ("Stepping");
        sfxSource.clip = stepSound;
        sfxSource.pitch = 0.64f;
        sfxSource.volume = 0.08f;
        sfxSource.Play ();

        Debug.Log (0.8f - movementSpeed / 60);
        yield return new WaitForSeconds (0.8f-movementSpeed/100);
        steppingSound = false;
    }
}
