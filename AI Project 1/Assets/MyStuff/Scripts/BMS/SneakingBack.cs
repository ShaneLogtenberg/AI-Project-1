using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;
using System.Linq;

public class SneakingBack : StateBehaviour
{
    ShyNPC nPC;

       public void OnEnable()
    {
        nPC = GetComponent<ShyNPC>();
        Invoke("Sneak", .1f);
    }

    void Sneak()
    {
        nPC.agent.isStopped = false;
        nPC.animator.SetBool("Walk", true);
        Invoke("UpdateHidingSpot", .5f);
    }

    public void UpdateHidingSpot()
    {
        Vector3[] bestSpots = nPC.hiddingSpots.Take(4).ToArray();

        Vector3 closestSpot = bestSpots.OrderBy(t => Vector3.Distance(this.transform.position, t)).FirstOrDefault();
        nPC.agent.destination = nPC.NavMeshLocation(closestSpot);        
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
