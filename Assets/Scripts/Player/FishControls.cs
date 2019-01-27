using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControls : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ( Input.GetKeyDown(KeyCode.Space) ) {
            StartCoroutine(roll());
        } else if ( Input.GetKeyDown(KeyCode.V) ) {
            StartCoroutine(jump());
        }
    }
    IEnumerator roll() {
        anim.SetBool("roll", true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("roll", false);
    }
    
    IEnumerator jump() {
        anim.SetBool("jump", true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("jump", false);
    }

}
