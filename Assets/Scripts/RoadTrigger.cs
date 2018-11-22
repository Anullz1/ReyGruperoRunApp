using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTrigger : MonoBehaviour {

    public Transform Objroad;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

  
    }

    private void OnTriggerEnter(Collider other)
    {
        // other.attachedRigidbody.velocity = new Vector3(0,10f,0); esto es un salto;
        Instantiate(Objroad, new Vector3(3.3f, -27.1f,transform.parent.position.z+200f),Objroad.rotation);
        transform.parent.gameObject.AddComponent<TimetoDestroy>();

       
    }
}
