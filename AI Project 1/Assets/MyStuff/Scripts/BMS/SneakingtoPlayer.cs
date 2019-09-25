using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class SneakingtoPlayer : StateBehaviour
{
    AllNPC nPC;
    public Vector3 playerBack;
    public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        nPC.agent.speed = 2;
    }
    public void UpdatePlayerPosition(Vector3 curretplayerFront)
    {
        nPC.agent = GetComponent<NavMeshAgent>();
        playerBack = curretplayerFront - new Vector3(0, 0, 4);
        nPC.agent.destination = playerBack;
    }

    // Called when the state is disabled
    void OnDisable()
    {
        nPC.agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        nPC.agent.destination = nPC.blackboard.GetVector3Var("Point1").Value;
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.2f)
            SendEvent("REACHPLAYER");
        nPC.agent.isStopped = false;
    }
}