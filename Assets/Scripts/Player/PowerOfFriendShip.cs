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
    public AudioSource sfxSource;
    public AudioClip chargingSound;
    private DeathController deathController;

    int totalPlayersInRadius = 0;
    float friendshipTimer;
    bool slightFriendship = false;
    bool charging = false;
    bool fullFriendship = false;
    public GameManager gm;
    // Start is called before the first frame update
    void Start () {
        friendshipTimer = timeUntilFriendshipPower;
        deathController = GetComponent<DeathController> ();
    }

    // Update is called once per frame
    void Update () {
        if (deathController.isDead) {
            Transform cylinder = transform.Find ("Cylinder(Clone)");
            if (cylinder != null) {
                cylinder.gameObject.GetComponent<TheHolyLight> ().kill ();
            }
            slightFriendship = false;
            charging = false;
            fullFriendship = false;
            friendshipTimer = timeUntilFriendshipPower;
            sfxSource.volume = 0f;
            sfxSource.Stop ();
            return;
        }

        totalPlayersInRadius = 0;
        // Create an array of hitColliders of objects within a certain radius of gameObject
        Collider [] hitColliders = Physics.OverlapSphere (transform.position, radius);
        // Fake for loop implementation
        int i = 0;
        while (i < hitColliders.Length) {
            // Check if collider is a player
            if (hitColliders [i].tag == "SmallFish") {
                if (!hitColliders [i].GetComponent<DeathController> ().isDead) {
                    totalPlayersInRadius++;
                }
                
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
                sfxSource.volume = 0.5f;
                sfxSource.clip = chargingSound;
                sfxSource.time = 7f;
                sfxSource.Play ();
            }
            
            Debug.Log ("Charging");
            friendshipTimer -= Time.deltaTime;
            if (friendshipTimer <= 0.0f) {
                Debug.Log ("POWER OF PowerOfFriendShip!!!!");
                fullFriendship = true;
                gm.playerWin ();
            }
            slightFriendship = false;
            charging = true;
            
        }        // Two players
        else if (totalPlayersInRadius >= reqPlayersToPower - 1) {
            // First time entering slight poweroffriendship
            if(!slightFriendship || charging) {
                Transform cylinder = transform.Find ("Cylinder(Clone)");
                if (cylinder != null) {
                    cylinder.gameObject.GetComponent<TheHolyLight> ().kill ();
                }
                GameObject cylinderNew = Instantiate (lightCylinder, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity) as GameObject;
                cylinderNew.transform.parent = gameObject.transform;
                cylinderNew.gameObject.GetComponent<TheHolyLight> ().setSlightPower ();
            }
            // Add PowerOfFriendShip
            Debug.Log ("Slight PowerOfFriendShip!!!!");
            friendshipTimer = timeUntilFriendshipPower;
            slightFriendship = true;
            charging = false;
            fullFriendship = false;
            sfxSource.volume = 0f;
            sfxSource.Stop ();
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
            sfxSource.volume = 0f;
            sfxSource.Stop ();
        }

    }
}
