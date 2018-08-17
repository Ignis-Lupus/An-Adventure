using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ally : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform head;
    public Animator anim;
    public Transform FP;
    public Transform face;
    Transform enemyLocation;

    public GameObject[] enemy;

    string state = "patrol";
    float rotSpeed = 1f;
    float speed = 3f;
    float accuracyWP = 0.1f;

    public FormationManager FM;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Move();
        

    }

    void Move()
    {

        //Vector3 direction = face.position - transform.position;
        //direction.y = 0;
        //float angle = Vector3.Angle(direction, transform.forward);

        //agent.SetDestination(FP.transform.position);
        //transform.LookAt(face);
        

        Face();
    }

    void Face()
    {

        if(FM.state == "ShieldWall")
        {

            Vector3 direction = face.position - transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(direction, transform.forward);

            agent.SetDestination(FP.transform.position);
            transform.LookAt(face);
            

        }


        if(FM.state == "relaxed")
        {

            agent.SetDestination(enemyLocation.transform.position);

        }
    }

    void FindEnemy()
    {
        if(enemy == null)
        {

            enemy = GameObject.FindGameObjectsWithTag("Enemy");

        }
        

    }

}

