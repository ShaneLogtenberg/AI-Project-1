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
        Invoke("GetMoving",0.01f);
        
    }

    void GetMoving()
    {
        nPC.agent.autoBraking = false;
        nPC.agent.speed = blackboard.GetFloatVar("Wander Speed").Value;
        nPC.animator.SetBool("Walk", true);

        GotoNextPoint();
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized * radius;
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
        point = RandomNavmeshLocation(10f);
        nPC.agent.SetDestination(point);
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.5f)
            GotoNextPoint();
        nPC.agent.isStopped = false;
    }
}
