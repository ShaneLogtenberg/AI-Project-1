using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class SneakingtoPlayer : StateBehaviour
{
    private NavMeshAgent agent;
    public Vector3 playerBack;
    public void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2;
    }
    public void UpdatePlayerPosition(Vector3 curretplayerFront)
    {
        agent = GetComponent<NavMeshAgent>();
        playerBack = curretplayerFront - new Vector3(0, 0, 4);
        agent.destination = playerBack;
    }

    // Called when the state is disabled
    void OnDisable()
    {
        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
            SendEvent("REACHPLAYER");
        agent.isStopped = false;
    }
}