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
    bool hideNotRun;
       public void OnEnable()
    {
        nPC = GetComponent<ShyNPC>();
        Invoke("Run", .1f);
    }

    void Run()
    {
        hideNotRun = false;
        nPC.agent.speed = 5f;
        nPC.agent.autoBraking = false;
        nPC.agent.isStopped = false;
        nPC.animator.SetBool("Walk", true);
        RunOppositeDirection();
    }

    void RunOppositeDirection()
    {
        oppositeDirection = (nPC.player.transform.position - transform.position).normalized * -5f;
        oppositeDirection.y = 0;
        pointDestination = nPC.NavMeshLocation(transform.position + oppositeDirection);

    }

    public void UpdateHidingSpot()
    {
        hideNotRun = true;
        GameObject[] testedSpots = nPC.hiddingSpots;
        GameObject closestSpot = testedSpots.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();

            while (nPC.playerVision.PointInSight(closestSpot))
            {
                testedSpots = testedSpots.Where(t => t != closestSpot).ToArray();
                closestSpot = testedSpots.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
            }
            pointDestination = nPC.NavMeshLocation(closestSpot.transform.position);            
    }

    public void UpdatePath()
    {

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
        //if (!nPC.agent.pathPending && nPC.playerVision.PointInSight(nPC.agent.destination))
        //{
        //    if (hideNotRun)
        //    {
        //        RunOppositeDirection();
        //    }
        //}
        Debug.DrawLine(transform.position, pointDestination);

        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);

        nPC.agent.destination = pointDestination;

        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < .7f)
        {
            if (hideNotRun)
            {
                nPC.agent.autoBraking = true;
                SendEvent("INREACH");
            }
            else
            {
                UpdateHidingSpot();
            }
        }
    }
}
