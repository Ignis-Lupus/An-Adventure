using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour {

    string state = "CanTakeDamage";

    static Animator anim;
    public float speed = 10.0f;
    public float health = 100;
    public bool dead = false;
    public Slider healthSlider;

    public DetectBlock DB;

	// Use this for initialization
	void Start ()
    {

        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        Move();
        AttackBlock();
        Health();
        UpdateUI();

        if(Input.GetKeyDown("escape"))
        {

            Cursor.lockState = CursorLockMode.None;

        }

        
	}

    void Move()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (translation != 0)
        {

            anim.SetBool("isWalking", true);
            //anim.SetBool("isIdle", false);

        }
        else
        {

            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);

        }

    }

    void AttackBlock()
    {

        if (Input.GetButton("Melee"))
        {

            anim.SetBool("isAttacking", true);
            //anim.SetBool("isIdle", false);

        }
        else
        {

            anim.SetBool("isAttacking", false);

        }

        if(Input.GetButton("Block"))
        {

            anim.SetBool("isBlocking", true);
            state = "CantTakeDamage";

        }
        else
        {

            anim.SetBool("isBlocking", false);
            state = "CanTakeDamage";

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShambleFist")
        {

            Debug.Log("Hit");
            if(state == "CanTakeDamage")
            {

                health -= 20;

            }
            
            UpdateUI();

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

    void UpdateUI()
        {

        //healthSlider.value = health;

        }

}
