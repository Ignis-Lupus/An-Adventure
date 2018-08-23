using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCircle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform avoidEnemy;


    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "EnemyRadius")
        {

            avoidEnemy = other.transform;

        }

    }
}
