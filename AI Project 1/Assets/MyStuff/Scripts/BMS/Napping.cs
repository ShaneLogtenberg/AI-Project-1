using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Napping : StateBehaviour
{
    AllNPC nPC;

    float napTime;
    bool wakingUp;

    private void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        napTime = nPC.napTime;
        nPC.agent.isStopped = true;
        nPC.animator.SetTrigger("Tired");
    }

    void Update()
    {
        napTime -= 1 * Time.deltaTime;

        if (napTime < 0 && !wakingUp)
        {
            WakeUp();
        }

        if(nPC.HasFoundFood == true)
        {
            WakeUp();
        }
    }

    void WakeUp()
    {
        wakingUp = true;
        nPC.animator.SetTrigger("Awake");
        nPC.awakeTime = nPC.maxAwakeTime;
    }

    public void FinishedAnimation()
    {
        nPC.blackboard.SendEvent("AWAKENED");
    }

    private void OnDisable()
    {
        nPC.agent.isStopped = false;
    }
}
