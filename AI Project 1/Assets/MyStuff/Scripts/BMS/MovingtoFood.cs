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
        nPC.agent.speed = 5f;
        InvokeRepeating("UpdateFoodPosition", 0, .5f);
    }
    public void UpdateFoodPosition(Vector3 curretFood)
    {
        nPC.foodThatIsFound.transform.position = curretFood;
        nPC.agent.destination = nPC.NavMeshLocation(curretFood);
        nPC.animator.SetBool("Walk", true);
    }

    // Called when the state is disabled
    void OnDisable()
    {
        nPC.agent.isStopped = true;
        nPC.agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.7f)
            CancelInvoke("UpdateFoodPosition");
            SendEvent("REACHPLAYER");
    }
}
