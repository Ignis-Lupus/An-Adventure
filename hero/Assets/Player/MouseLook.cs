using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    Vector3 mouseLook;
    Vector3 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject character;
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

        var md = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector3.Scale(md, new Vector3(sensitivity * smoothing, sensitivity * smoothing));
        
        smoothV.x = Mathf.Lerp(smoothV.x, md.x,  1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        character.transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);


    }

}
