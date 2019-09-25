using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Napping : StateBehaviour
{
    AllNPC nPC;
    Transform body;
    public float napTime;

    private void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        napTime = blackboard.GetFloatVar("Nap Time").Value;
        nPC.agent.isStopped = true;
        nPC.animator.SetTrigger("Tired");
    }

    void Update()
    {
        napTime -= 1 * Time.deltaTime;

        if (napTime < 0)
        {
            gameObject.SendMessage("WakeUp");
        }
    }

    void WakeUp()
    {
        nPC.animator.SetTrigger("Awake");
    }

}
