using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;
using System.Linq;

public class Retreating : StateBehaviour
{
    ShyNPC nPC;

    Vector3 oppositeDirection;
       public void OnEnable()
    {
        nPC = GetComponent<ShyNPC>();
        Invoke("Run", .1f);
    }

    void Run()
    {
        nPC.agent.speed = 5f;
        nPC.agent.isStopped = false;
        nPC.animator.SetBool("Walk", true);
        oppositeDirection = (transform.position - nPC.player.transform.position).normalized * 5f;
        nPC.agent.destination = nPC.NavMeshLocation(oppositeDirection);
        Invoke("UpdateHidingSpot", .5f);
    }

    public void UpdateHidingSpot()
    {
        Vector3[] testedSpots = nPC.hiddingSpots;
        Vector3 finalPoint = Vector3.zero;
        Vector3 closestSpot = testedSpots.OrderBy(t => Vector3.Distance(transform.position, t)).FirstOrDefault();

        while (!nPC.playerVision.PointInSight(nPC.NavMeshLocation(closestSpot)))
        {
            testedSpots = testedSpots.Where(t => t != closestSpot).ToArray();
            closestSpot = testedSpots.OrderBy(t => Vector3.Distance(transform.position, t)).FirstOrDefault();
        }
            nPC.agent.destination = nPC.NavMeshLocation(finalPoint);
        
    }


    // Called when the state is disabled
    void OnDisable()
    {
        CancelInvoke("UpdateHidingSpot");
        nPC.agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.7f)
        {
            CancelInvoke("UpdateHidingSpot");
            SendEvent("INREACH");
        }
    }
}
