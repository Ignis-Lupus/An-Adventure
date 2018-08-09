using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseNPC {

    public GameObject[] waypoints;
    int currentWP;

    public UnityEngine.AI.NavMeshAgent agent;


    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWP = 0;

    }

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0) return;
        if(Vector3 .Distance(waypoints[currentWP].transform.position, NPC.transform.position) < 3.0f)
        {

            currentWP = Random.Range(0, waypoints.Length);

        }

        agent.SetDestination(waypoints[currentWP].transform.position);

    }

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
