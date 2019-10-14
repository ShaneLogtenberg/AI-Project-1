using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Wandering : StateBehaviour
{
    public Vector3 point;

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
        nPC.agent.isStopped = false;
        nPC.agent.speed = blackboard.GetFloatVar("Wander Speed").Value;
        nPC.animator.SetBool("Walk", true);
        point = blackboard.GetVector3Var("Destination").Value;

        GotoNextPoint();
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {

        Vector3 randomDirection = Quaternion.Euler(0,Random.Range(-45f,45f),0) * transform.forward * radius;
        randomDirection += transform.position;
        if (GameObject.Find("Bounds").GetComponent<Collider>().bounds.Contains(randomDirection))
        {
            randomDirection = randomDirection + ((Vector3.zero - randomDirection).normalized * (Vector3.zero - randomDirection).magnitude/2);
        }
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
        if (!nPC.agent.pathPending)
        {
            point = RandomNavmeshLocation(10f);
            blackboard.GetVector3Var("Destination").Value = point;
            nPC.agent.SetDestination(point);
        }
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 1f)
            GotoNextPoint();

        Debug.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, -45f, 0) * transform.forward * 10), Color.yellow);
        Debug.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 45f, 0) * transform.forward * 10), Color.yellow);
    }
}
