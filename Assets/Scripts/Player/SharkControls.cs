using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkControls : MonoBehaviour
{

    Animator anim;
    Rigidbody rb;
    Collider col;
    public float biteCooldown = 2.0f;
    
	private float impulsePower = 25;

    int playerNum = 0;
    bool canBite = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb   = GetComponent<Rigidbody>();
        col  = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool fire = Input.GetButtonDown("Fire" + playerNum.ToString());
        if ( Input.GetKeyDown(KeyCode.W) ) {
            StartCoroutine(running("start"));
        } else if (fire && canBite ) {
            Debug.Log("Biting");
            StartCoroutine(bite());
        }

        if ( Input.GetKeyUp(KeyCode.W) ) {
            StartCoroutine(running("stop"));
        } 
    }

    IEnumerator running(string s) {

        if ( s == "start" ) {
            anim.SetFloat("speedPercent", 10f);
            yield return new WaitForSeconds(0.0f);
        } else if ( s == "stop" ) {
            anim.SetFloat("speedPercent", 0.0f);
            yield return new WaitForSeconds(0.0f);
        } else {
            yield return new WaitForSeconds(0.0f);
        }
        
    }
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject);
		if ( collision.gameObject.tag == "SmallFish" && !canBite ) {
            // death event
            collision.gameObject.GetComponent<DeathController> ().die ();
            Debug.Log("kilt him");
		}
	}
    IEnumerator bite(){
        anim.SetFloat("speedPercent", 60);
        canBite = false;
        Debug.Log("false canbite");
        Vector3 Bite = transform.forward * impulsePower;
        rb.AddForce(Bite, ForceMode.Impulse);
		yield return new WaitForSeconds(biteCooldown);
		canBite = true;
        anim.SetFloat("speedPercent", 0);
        Debug.Log("true canbite");
    }
}
