using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Hidding : StateBehaviour
{
    private NavMeshAgent agent;
    public Vector3 destination;

    public void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = blackboard.GetVector3Var("Point1").Value;
        destination = agent.destination;
        agent.speed = 5;
    }


    // Called when the state is disabled
    void OnDisable()
    {
        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = blackboard.GetVector3Var("Point1").Value;
        destination = agent.destination;
        if (!agent.pathPending && agent.remainingDistance < 0.7f)
            agent.isStopped = true;
    }
}
