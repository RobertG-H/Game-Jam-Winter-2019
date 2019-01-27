using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBite : MonoBehaviour
{
	Controls parentScript;
	
	void Start() {
		Controls parentScript = GetComponent<Controls>();
	}
	
	void OnCollisionEnter(Collision col) {
		if ( col.gameObject.tag == "SmallFish" && parentScript.biting) {
			parentScript.biting = false; // death event
			Debug.Log("YOU ARE DEAD");
		}
	}
}
