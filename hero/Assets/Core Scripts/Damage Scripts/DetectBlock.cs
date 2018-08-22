using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlock : MonoBehaviour {

    public PlayerController PC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {

            Debug.Log("Blocked");

        }


    }
}
