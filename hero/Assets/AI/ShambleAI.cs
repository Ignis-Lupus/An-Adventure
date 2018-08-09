using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambleAI : MonoBehaviour {

    public GameObject player;
    Animator anim;

    public Transform _player;
    public Transform head;

    public GameObject GetPlayer()
    {

        return player;

    }

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = _player.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, head.forward);

        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));

	}
}
