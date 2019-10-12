using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;
using System;

public class Eating : StateBehaviour
{
    AllNPC nPC;

    public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        Invoke("Eat",0.01f);
    }

    private void Eat()
    {
        nPC.agent.isStopped = true;
        nPC.animator.SetTrigger("Eat");
    }

    public void Finished()
    {
        Destroy(nPC.foodThatIsFound);
        SendEvent("NOFOOD");
    }

    // Called when the state is disabled
    void OnDisable()
    {
        nPC.agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nPC.foodThatIsFound != null)
        {
            if (Vector3.Distance(nPC.foodThatIsFound.transform.position, transform.position) > 1.4f)
                SendEvent("OUTREACH");
        }
    }
}
