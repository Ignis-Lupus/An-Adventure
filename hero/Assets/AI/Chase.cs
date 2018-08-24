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

    float timeToGo;

    private Targetting T;
    private PlayerController PC;


    //public Slider healthSlider;

    public bool dead = false;
    public float health = 100;

    public GameObject damageCollider;
    

    // Use this for initialization
    void Start ()
    {

        T = FindObjectOfType<Targetting>();
        anim = GetComponent<Animator>();
        timeToGo = Time.fixedTime + 3.0f;

	}

    IEnumerator Damage()
    {
        Debug.Log("attacked");
        anim.SetBool("isAttacking", true);
        damageCollider.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        damageCollider.SetActive(false);
        anim.SetBool("isAttacking", false);
        StartCoroutine(Damage());

    }

    // Update is called once per frame
    void Update ()
    {
        if(dead == false)
        {

            Move();

        }


        


        Health();

	}

    void Target()
    {
        if(player == null)
        {

            

        }

        player = T.selectedTarget;

    }

    private void FixedUpdate()
    {
        
        if(Time.fixedTime >= timeToGo)
        {

            Target();
            timeToGo = Time.fixedTime + 3.0f;

        }

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

            //if (direction.magnitude > 5)
            //{
                
            //    anim.SetBool("isAttacking", false);
            //    anim.SetBool("isWalking", true);

            //}
            //else
            //{

            //    Debug.Log("attack");
            //    anim.SetBool("isAttacking", true);
            //    anim.SetBool("isWalking", false);

            //}

            if (direction.magnitude < 5)
            {

                StartCoroutine(Damage());
                
            }
            else
            {

                StopCoroutine(Damage());

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
