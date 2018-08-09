﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : StateMachineBehaviour {

    public GameObject NPC;
    public GameObject opponent;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        NPC = animator.gameObject;
        opponent = NPC.GetComponent<ShambleAI>().GetPlayer();

    }
}