using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProgress : MonoBehaviour
{
    public float startPosition = 2f;
    public float pos;

    // Start is called before the first frame update
    void Start()
    {
        setDeathProgressPosition(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setDeathProgressPosition (float percent)
    {
        pos = startPosition - startPosition * percent/100;
        transform.localPosition =  new Vector3(0.0f, pos, 0.0f);
    }
}