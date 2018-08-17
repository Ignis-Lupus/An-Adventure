using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour {

    public string state = "ShieldWall";
    public Transform player;


	// Use this for initialization
	void Start ()
    {
		


	}
	
	// Update is called once per frame
	void Update ()
    {

        Formation();

	}

    void Formation()
    {

        if(state == "relaxed")
        {



        }

        if(state == "ShieldWall")
        {



        }

    }

    void Control()
    {

        if(Input.GetButtonDown("Formation1"))
        {

            state = "relaxed";

        }

        if(Input.GetButtonDown("Formation2"))
        {

            state = "ShieldWall";

        }

    }
}
