using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOfFriendShip : MonoBehaviour
{
    // Public objects to change in editor
    public float radius = 5f;
    public int reqPlayersToPower = 3;

    int totalPlayersInRadius = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      totalPlayersInRadius = 0;
      // Create an array of hitColliders of objects within a certain radius of gameObject
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
      // Fake for loop implementation
      int i = 0;
      while (i < hitColliders.Length)
      {
          // Check if collider is a player
          if (hitColliders[i].tag == "Player") {
            Debug.Log("COLLIDING WITH PLAYER: ");
            Debug.Log(hitColliders[i].gameObject.name);
            totalPlayersInRadius++;
          }
          i++;
      }
      // If there are three players close together activate the power of PowerOfFriendShip
      if (totalPlayersInRadius + 1 == reqPlayersToPower) {
        // Add PowerOfFriendShip
        Debug.Log("POWER OF PowerOfFriendShip!!!!");
      }
    }
}
