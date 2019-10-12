using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class MovingtoPlayer : StateBehaviour
{
    AllNPC nPC;
    public Vector3 playerFront;
       public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        Invoke("GetMoving", .1f);
    }

    void GetMoving()
    {
        nPC.agent.speed = 5f;
        nPC.agent.isStopped = false;
        nPC.animator.SetBool("Walk", true);
        InvokeRepeating("UpdatePlayerPosition", 0, .5f);
    }

    public void UpdatePlayerPosition()
    {
        GameObject player = nPC.global.GetGameObjectVar("Player");
        playerFront = player.transform.GetChild(3).transform.position;
        nPC.agent.destination = nPC.NavMeshLocation(playerFront);
    }


    // Called when the state is disabled
    void OnDisable()
    {
        CancelInvoke("UpdatePlayerPosition");
        nPC.agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        nPC.animator.SetFloat("Speed", nPC.agent.velocity.magnitude);
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.7f)
        {
            CancelInvoke("UpdatePlayerPosition");
            SendEvent("INREACH");
        }
    }
}
