using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MveMax : MonoBehaviour
{
    private Rigidbody thisrigid;
    public float speed = 10f;
 

    // Use this for initialization
    void Start()
    {
        thisrigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        thisrigid.velocity = new Vector3(Input.acceleration.x, thisrigid.velocity.y, speed);
    }

}