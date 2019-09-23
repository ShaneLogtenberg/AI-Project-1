using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class MovingtoPlayer : StateBehaviour
{
    private NavMeshAgent agent;
    public Vector3 playerFront;
    public void UpdatePlayerPosition(Vector3 curretplayerFront)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = curretplayerFront;
        playerFront = curretplayerFront;
    }
    public void OnEnable()
    {
        agent.speed = 5f;
    }

    // Called when the state is disabled
    void OnDisable()
    {
        agent.isStopped = true;
        agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.7f)
            SendEvent("REACHPLAYER");
    }
}
