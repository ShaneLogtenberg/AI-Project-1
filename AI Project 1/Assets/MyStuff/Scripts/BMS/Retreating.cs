using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;
using System.Linq;

public class Retreating : StateBehaviour
{
    ShyNPC nPC;
    public Vector3 pointDestination;
    Vector3 oppositeDirection;
       public void OnEnable()
    {
        nPC = GetComponent<ShyNPC>();
        Invoke("Run", .1f);
    }

    void Run()
    {
        nPC.agent.speed = 5f;
        nPC.agent.autoBraking = false;
        nPC.agent.isStopped = false;
        nPC.animator.SetBool("Walk", true);
        oppositeDirection = (transform.position - nPC.player.transform.position).normalized * 5f;
        pointDestination = nPC.NavMeshLocation(oppositeDirection);
    }

    public void UpdateHidingSpot()
    {
        Vector3[] testedSpots = nPC.hiddingSpots;
        Vector3 closestSpot = testedSpots.OrderBy(t => Vector3.Distance(nPC.player.transform.position, t)).LastOrDefault();

            while (nPC.playerVision.PointInSight(closestSpot))
            {
                testedSpots = testedSpots.Where(t => t != closestSpot).ToArray();
                closestSpot = testedSpots.OrderBy(t => Vector3.Distance(nPC.player.transform.position, t)).LastOrDefault();
        }
            pointDestination = closestSpot;            
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
        if (nPC.playerVision.PointInSight(nPC.agent.destination))
        {
            UpdateHidingSpot();
        }

        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);

        if (!nPC.agent.pathPending && nPC.agent.destination != pointDestination)
        nPC.agent.destination = pointDestination;

        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < .7f)
        {
            nPC.agent.autoBraking = true;
            SendEvent("OUTFOUCUS");
        }
    }
}
