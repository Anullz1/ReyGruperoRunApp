using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimetoDestroy : MonoBehaviour {

    public float destroytime = 15f;
    public float currentTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentTime += Time.deltaTime;

        if(currentTime >destroytime)
        {
            Destroy(gameObject);
        }
	}
}
