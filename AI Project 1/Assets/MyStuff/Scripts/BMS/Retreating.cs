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

        while (!nPC.player.GetComponent<Vision>().PointInSight(nPC.NavMeshLocation(closestSpot)))
        {
            testedSpots = testedSpots.Where(t => t != closestSpot).ToArray();
            closestSpot = testedSpots.OrderBy(t => Vector3.Distance(transform.position, t)).FirstOrDefault();
        }
            nPC.agent.destination = nPC.NavMeshLocation(finalPoint);
        
    }

    //Vector3 ClosestHidingSpot(Vector3[] testedSpots)
    //{
    //    Vector3 closestSpot = Vector3.zero;
    //    float smallestDistance = Mathf.Infinity;

    //    foreach (var testedSpot in testedSpots)
    //    {
    //        var tempDist = Vector3.Distance(nPC.agent.destination, testedSpot);
    //        if (tempDist < smallestDistance)
    //        {
    //            smallestDistance = tempDist;
    //            closestSpot = testedSpot;
    //        }
    //    }
    //    return closestSpot;

    //    //if (testedSpots.Length > 0) {
    //    //    Vector3 closestSpot = testedSpots[0];
    //    //    var dist = Vector3.Distance(nPC.agent.destination, testedSpots[0]);
    //    //    for (var i = 0; i < nPC.hiddingSpots.Length; i++) {
    //    //        var tempDist = Vector3.Distance(nPC.agent.destination, testedSpots[i]);
    //    //        if (tempDist < dist) {
    //    //            closestSpot = nPC.hiddingSpots[i];
    //    //        }
    //    //    }
    //    //    return closestSpot;
    //    //}
    //    //return testedSpots[0];
    //}

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
