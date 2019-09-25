using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Hidding : StateBehaviour
{
    AllNPC nPC;
    public Vector3 destination;

    public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        nPC.agent.destination = blackboard.GetVector3Var("Point1").Value;
        destination = nPC.agent.destination;
        nPC.agent.speed = 5;
    }


    // Called when the state is disabled
    void OnDisable()
    {
        nPC.agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        //nPC.agent.destination = nPC.blackboard.GetVector3Var("Point1").Value;
        destination = nPC.agent.destination;
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.7f)
            nPC.agent.isStopped = true;
    }
}
