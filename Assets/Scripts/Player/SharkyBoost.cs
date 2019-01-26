using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkyBoost : MonoBehaviour
{
    
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.W)){
            Debug.Log("is working?");
            rb.AddForce(transform.forward * 20, ForceMode.Impulse);
        }
        
    }
}
