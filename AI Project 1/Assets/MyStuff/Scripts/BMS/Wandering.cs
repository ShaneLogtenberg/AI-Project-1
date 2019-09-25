using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Wandering : StateBehaviour
{
    public Vector3 point;
    private int destPoint = 0;
    AllNPC nPC;

    void Start()
    {
        nPC = GetComponent<AllNPC>();
    }

    private void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        nPC.agent.autoBraking = false;
        nPC.agent.speed = blackboard.GetFloatVar("Wander Speed").Value;     


        GotoNextPoint();
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    void GotoNextPoint()
    {
        point = RandomNavmeshLocation(5f);
        nPC.agent.SetDestination(point);
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        nPC.agent.destination = blackboard.GetVector3Var("Point1").Value;
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.5f)
            GotoNextPoint();
        nPC.agent.isStopped = false;
    }
}
