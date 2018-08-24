using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOne : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject shamble;
    public GameObject shamble1;
    public GameObject shamble2;
    public GameObject shamble3;

    public Transform spawn;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Human")
        {

            shamble.SetActive(true);
            shamble1.SetActive(true);
            shamble2.SetActive(true);
            shamble3.SetActive(true);

            gameObject.SetActive(false);

        }

    }
}

