using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class MovingtoFood : StateBehaviour
{
    AllNPC nPC;

    public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        //nPC.agent.speed = 5f;
        InvokeRepeating("UpdateFoodPosition", 0, .5f);
    }
    public void UpdateFoodPosition()
    {
        if (nPC.foodThatIsFound != null)
        {
            nPC.agent.destination = nPC.NavMeshLocation(nPC.foodThatIsFound.transform.position);
            nPC.animator.SetBool("Walk", true);
        }
        else
        {
            SendEvent("NOFOOD");
        }
    }

    // Called when the state is disabled
    void OnDisable()
    {
        CancelInvoke("UpdateFoodPosition");
        nPC.agent.isStopped = true;
        nPC.agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 1.4f)
        {
            SendEvent("INREACH");
        }
    }
}
