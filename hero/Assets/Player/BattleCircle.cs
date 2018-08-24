using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCircle : MonoBehaviour {

    public Transform avoidEnemy;

    public Chase chase;
    public PlayerController PC;
    public Ally human;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "EnemyRadius")
        {

            avoidEnemy = other.transform;

        }

        if (other.tag == "Human")
        {



        }

    }
}
