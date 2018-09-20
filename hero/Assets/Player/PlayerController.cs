using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class PlayerController : MonoBehaviour {

    string state = "CanTakeDamage";

    public Flowchart Flowchart;

    static Animator anim;
    public float speed = 4.0f;
    public float health = 100;
    public bool dead = false;
    public Slider healthSlider;

    public bool cursorLocked;
    public bool underAttack = false;

    public GameObject spawn;

    public GameObject damageCollider;

    public DetectBlock DB;

    bool hasSword = false;
    bool hasShield = false;
    bool canPickUp = false;
    bool canPickUpShield = false;

    public GameObject worldSword;
    public GameObject playerSword;
    public GameObject worldShield;
    public GameObject playerShield;

    public GameObject door;

    // Use this for initialization
    void Start ()
    {

        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        cursorLocked = true;

		
	}
	
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("rollForward", false);
        anim.SetBool("rollBackward", false);
        anim.SetBool("rollRight", false);
        anim.SetBool("rollLeft", false);
        speed = 4.0f;
        

    }

    IEnumerator Damage()
    {

        anim.SetBool("isAttacking", true);
        damageCollider.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        damageCollider.SetActive(false);
        anim.SetBool("isAttacking", false);

    }

	// Update is called once per frame
	void Update ()
    {

        if(dead != true)
        {

            Move();

        }
        
        AttackBlock();
        Health();
        UpdateUI();

        if(Input.GetKeyDown("escape") && cursorLocked == true)
        {

            Cursor.lockState = CursorLockMode.None;
            cursorLocked = false;

        }

        if (Input.GetKeyDown("escape") && cursorLocked == false)
        {
        
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;

        }


        if (hasShield == true && hasSword == true)
        {

            door.SetActive(false);

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

        if (Input.GetButtonDown("Roll") && Input.GetButton("Forward"))
        {

            anim.SetBool("rollForward", true);
            speed = 10.0f;
            StartCoroutine(Wait());

        }


        if (Input.GetButtonDown("Roll") && Input.GetButton("Backward"))
        {

            anim.SetBool("rollBackward", true);
            speed = 10.0f;
            StartCoroutine(Wait());

        }

        if (Input.GetButtonDown("Roll") && Input.GetButton("Right"))
        {

            anim.SetBool("rollRight", true);
            speed = 10.0f;
            StartCoroutine(Wait());

        }

        if (Input.GetButtonDown("Roll") && Input.GetButton("Left"))
        {

            anim.SetBool("rollLeft", true);
            speed = 10.0f;
            StartCoroutine(Wait());

        }
    }



    void AttackBlock()
    {

        if(Input.GetButtonDown("PickUpSword") && canPickUp == true)
        {

            worldSword.SetActive(false);
            playerSword.SetActive(true);
            hasSword = true;

        }

        if (Input.GetButtonDown("PickUpShield") && canPickUpShield == true)
        {

            worldShield.SetActive(false);
            playerShield.SetActive(true);
            hasShield = true;

        }

        if (Input.GetButton("Melee") && hasSword == true)
        {

            
            StartCoroutine(Damage());

        }

        if(Input.GetButton("Block") && hasShield == true)
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


            if(state == "CanTakeDamage")
            {

                health -= 20;

            }
            
            UpdateUI();

        }


    }

    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "PickUPSword")
        {

            canPickUp = true;

        }
        else
        {

            canPickUp = false;

        }

        if (other.tag == "PickUpShield")
        {

            canPickUpShield = true;

        }
        else
        {

            canPickUpShield = false;

        }

    }
    
    

    void Health()
    {

        if (health <= 0)
        {

            anim.SetBool("isDead", true);
            dead = true;
            Invoke("Death", 5.0f);
            //Flowchart.ExecuteBlock("Dead");

        }

    }


    void Death()
    {

        anim.SetBool("isDead", false);
        transform.position = spawn.transform.position;
        Invoke("Respawn", 0.1f);

    }

    void Respawn()
    {

        health = 100;
        dead = false;

    }
   
    void UpdateUI()
    {

        healthSlider.value = health;

    }
}

    


