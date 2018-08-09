using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

    public Transform player;
    public Transform head;

    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    float rotSpeed = 0.2f;
    float speed = 1.5f;
    float accuracyWP = 3.0f;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        Follow();

	}

    void Follow()
    {

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, head.forward);

        if (state == "patrol" && waypoints.Length > 0)
        {

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {

                currentWP++;
                if(currentWP >= waypoints.Length)
                {

                    currentWP = 0;

                }

                direction = waypoints[currentWP].transform.position.normalized - transform.position;
                transform.rotation. = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                transform.Translate(0, 0, Time.deltaTime * speed);

            }

        }

        if (Vector3.Distance(player.position, transform.position) < 10 && (angle < 30 || pursuing))
        {

            

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            if(direction.magnitude > 2)
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

            pursuing = false;

        }

    }
}
