using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class MovingtoPlayer : StateBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdatePlayerPosition(Vector3 playerFront)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerFront;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.7f)
            agent.isStopped = true;
    }
}
