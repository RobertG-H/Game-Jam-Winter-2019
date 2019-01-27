using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public bool isDead = false;
    public float revivePercent = 0f;
    public float radius = 5f;

    public int REVIVE_TIME = 5;

    private DeathProgress deathProgress;

    void Start()
    {
        deathProgress =  GetComponentInChildren<DeathProgress>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && isNearAlivePlayer())
        {
            revivePercent += Time.deltaTime * 100 / REVIVE_TIME;
            if (revivePercent >= 100)
                isDead = false;
        }
        else
        {
            revivePercent = 0; 
        }
        deathProgress.GetComponent<MeshRenderer>().enabled = isDead;
        deathProgress.setDeathProgressPosition(revivePercent);
    }

    bool isNearAlivePlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Player" && !hitColliders[i].GetComponent<DeathController>().isDead)
                return true;
        }
        return false;
    }
}
