using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOfFriendShip : MonoBehaviour
{

    public GameObject lightCylinder;
    // Public objects to change in editor
    [SerializeField]
    private float radius = 5f;
    private int reqPlayersToPower = 3;
    private float timeUntilFriendshipPower = 5f;

    int totalPlayersInRadius = 0;
    float friendshipTimer;
    bool slightFriendship = false;
    bool charging = false;
    bool fullFriendship = false;
    // Start is called before the first frame update
    void Start () {
        friendshipTimer = timeUntilFriendshipPower;
    }

    // Update is called once per frame
    void Update () {
        totalPlayersInRadius = 0;
        // Create an array of hitColliders of objects within a certain radius of gameObject
        Collider [] hitColliders = Physics.OverlapSphere (transform.position, radius);
        // Fake for loop implementation
        int i = 0;
        while (i < hitColliders.Length) {
            // Check if collider is a player
            if (hitColliders [i].tag == "SmallFish") {
                totalPlayersInRadius++;
            }
            i++;
        }
        // If there are three players close together activate the power of PowerOfFriendShip    
        if (totalPlayersInRadius >= reqPlayersToPower) {
            if (!charging) {
                Transform cylinder = transform.Find ("Cylinder(Clone)");
                if (cylinder != null) {
                    cylinder.gameObject.GetComponent<TheHolyLight> ().setCharging ();
                }
            }
            
            Debug.Log ("Charging");
            friendshipTimer -= Time.deltaTime;
            if (friendshipTimer <= 0.0f) {
                Debug.Log ("POWER OF PowerOfFriendShip!!!!");
                fullFriendship = true;
            }
            slightFriendship = false;
            charging = true;
            
        }        // Two players
        else if (totalPlayersInRadius >= reqPlayersToPower - 1) {
            // First time entering slight poweroffriendship
            if(!slightFriendship) {
                //GameObject cylinder = Instantiate (lightCylinder, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity) as GameObject;
                //cylinder.transform.parent = gameObject.transform;
                //cylinder.gameObject.GetComponent<TheHolyLight> ().setSlightPower ();
            }
            // Add PowerOfFriendShip
            Debug.Log ("Slight PowerOfFriendShip!!!!");
            friendshipTimer = timeUntilFriendshipPower;
            slightFriendship = true;
            charging = false;
            fullFriendship = false;
        }
        // No players
        else {
            Transform cylinder = transform.Find ("Cylinder(Clone)");
            if (cylinder != null) {
                cylinder.gameObject.GetComponent<TheHolyLight> ().kill ();
            }
            slightFriendship = false;
            charging = false;
            fullFriendship = false;
            friendshipTimer = timeUntilFriendshipPower;
        }

    }
}
