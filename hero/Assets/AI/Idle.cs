using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{

    public Transform player;
    public Transform head;
    public UnityEngine.AI.NavMeshAgent agent;

    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    float rotSpeed = 0.2f;
    float speed = 1.5f;
    float accuracyWP = 2.0f;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Follow();

    }

    void Follow()
    {

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, head.forward);

        //if (state == "patrol" && waypoints.Length > 0)
        //{


        //    if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
        //    {

        //        currentWP = Random.Range(0, waypoints.Length);

        //        //currentWP++;
        //        //if(currentWP >= waypoints.Length)
        //        //{

        //        //    currentWP = 0;

        //        //}

        //        Debug.Log("Patroling");
        //    }

        //    agent.SetDestination(waypoints[currentWP].transform.position);
        //    anim.SetBool("isIdle", false);
        //    anim.SetBool("isWalking", true);
        //}

        if (Vector3.Distance(player.position, transform.position) < 10 && (angle < 30 || state == "pursuing"))
        {

            state = "pursuing";

            agent.SetDestination(player.transform.position);


            if (direction.magnitude > 2)
            {

                transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);

            }
            else
            {

                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);

            }


        }
        else
        {

            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);

            //state = "patrol";

        }

    }
}
