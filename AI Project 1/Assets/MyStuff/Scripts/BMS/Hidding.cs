using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Hidding : StateBehaviour
{
    ShyNPC nPC;
    public Vector3 destination;

    public void OnEnable()
    {
        nPC = GetComponent<ShyNPC>();
        Invoke("Hide", .1f);
    }

    void Hide()
    {
        nPC.animator.SetBool("Walk", false);
        nPC.agent.speed = 2;
        nPC.agent.isStopped = true;
    }

    // Called when the state is disabled
    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!nPC.agent.pathPending && nPC.agent.remainingDistance < 0.7f)
            nPC.agent.isStopped = true;
    }
}
