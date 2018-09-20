using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    Vector3 mouseLook;
    Vector3 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject character;
    public GameObject Camera;
    public float turnSpeed = 1f;

    float minRotation = -45;
    float maxRotation = 45;
    
    
    
	// Use this for initialization
	void Start ()
    {

        //character = transform.parent.gameObject;

	}
	
	// Update is called once per frame
	void Update ()
    {

        var rotY = new Quaternion(Mathf.Clamp(transform.rotation.y, minRotation, maxRotation), 0, 0, 0);

        character.transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
        transform.Rotate(Input.GetAxis("Mouse Y") * turnSpeed, 0, 0);

        

    }

}
