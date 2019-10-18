using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Napping : StateBehaviour
{
    AllNPC nPC;
    
    public float napTime;
    public bool wakingUp;
    public bool layingdownDown;

    private void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        napTime = nPC.napTime;
        nPC.agent.isStopped = true;
        wakingUp = false;
        nPC.animator.SetTrigger("Tired");
    }

    void Update()
    {
        if (layingdownDown)
        {
            napTime -= 1 * Time.deltaTime;
        }

        if (napTime < 0 && !wakingUp)
        {
            WakeUp();
        }

        if(nPC.HasFoundFood == true && !wakingUp)
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

    public void LaydownDone()
    {
        layingdownDown = true;
    }

    public void GetUpDone()
    {
        if (GetComponent<FriendlyNPC>() != null)
        {
            GetComponent<FriendlyNPC>().ResetHunger();
        }
        nPC.blackboard.SendEvent("AWAKENED");
    }

    private void OnDisable()
    {
        nPC.agent.isStopped = false;
    }
}
