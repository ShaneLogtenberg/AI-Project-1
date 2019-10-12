﻿using System.Collections;
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
        nPC.agent.speed = 5f;
        nPC.animator.SetBool("Walk", true);
        InvokeRepeating("UpdatePlayerPosition", 0, .5f);
    }
    public void UpdatePlayerPosition(Vector3 curretplayerFront)
    {
        curretplayerFront = nPC.NavMeshLocation(curretplayerFront);
        nPC.agent.destination = curretplayerFront;
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
            CancelInvoke("UpdatePlayerPosition");
            SendEvent("REACHPLAYER");
    }
}
