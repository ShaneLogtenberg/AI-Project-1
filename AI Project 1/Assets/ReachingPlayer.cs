using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class ReachingPlayer : StateBehaviour
{
    private NavMeshAgent agent;

    // Called when the state is enabled
    void OnEnable () {
        agent = GetComponent<NavMeshAgent>();
        MovingtoPlayer movingtoPlayer = GetComponent<MovingtoPlayer>();
        agent.destination = movingtoPlayer.playerFront;
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


