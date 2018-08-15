using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chase : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public Transform head;
    public Animator anim;
    bool pursuing = false;
    

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP;
    float rotSpeed = 1f;
    float speed = 3f;
    float accuracyWP = 5.0f;
    


    //public Slider healthSlider;

    public bool dead = false;
    public float health = 100;
    

    // Use this for initialization
    void Start ()
    {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(dead == false)
        {

            Move();
            Debug.Log("can move");
        }

        Health();

	}

    void Move()
    {

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);

        if (state == "patrol" && waypoints.Length > 0)
        {

            //anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {

                currentWP = Random.Range(0, waypoints.Length);

            }

            agent.SetDestination(waypoints[currentWP].transform.position);

        }

        if (Vector3.Distance(player.position, transform.position) < 10 && (angle < 30 || state == "pursuing"))
        {

            state = "pursuing";

            agent.SetDestination(player.transform.position);

            if (direction.magnitude > 5)
            {

                //transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);

            }
            else
            {

                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);

            }

        }
        else
        {

            //anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);
            state = "patrol";

        }

    }

    void Health()
    {

        if (health <= 0)
        {

            anim.SetBool("isDead", true);
            dead = true;
            Debug.Log("dead");
            Invoke("Death", 10.0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AlliedWeapon")
        {

            Debug.Log("Hit");
            health -= 100;
            UpdateUI();

        }

        if(other.tag == "PlayerWeapon")
        {

            Debug.Log("Hit");
            health -= 100;
            UpdateUI();

        }
        

    }
    void UpdateUI()
    {

        //healthSlider.value = health;

    }

    void Death()
    {

        Destroy(gameObject);

    }

}
