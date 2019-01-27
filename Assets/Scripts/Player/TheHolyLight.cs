using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHolyLight : MonoBehaviour
{
    public float growSpeed = 10;
    public float startingScale = 1;
    public float targetScale = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void kill () {
        Destroy (gameObject);
    }

    public void setSlightPower() {
        Transform firstChild = transform.GetChild (0);
        firstChild.gameObject.SetActive (false);
        transform.localScale = new Vector3(transform.localScale.x,2, transform.localScale.z);
    }

    public void setCharging() {
        Transform firstChild = transform.GetChild (0);
        firstChild.gameObject.SetActive (true);
        transform.localScale = new Vector3 (transform.localScale.x, 100, transform.localScale.z);
    }

    // Must be called in update
    public void grow() {
        if (transform.localScale.x < targetScale) {
            float newScale = Time.deltaTime * growSpeed;
            transform.localScale += new Vector3 (newScale, newScale, newScale);
        }
    }
}
