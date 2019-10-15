﻿using System.Collections;
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
        nPC.foodThatIsFound.transform.parent = this.gameObject.transform;
        nPC.animator.SetTrigger("Eat");
    }

    public void Finished()
    {
        Destroy(nPC.foodThatIsFound);
        nPC.HasFoundFood = false;
        SendEvent("NOFOOD");
        if(GetComponent<FriendlyNPC>()!= null)
        {
            GetComponent<FriendlyNPC>().hangry = false;
        }
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
            Vector3 newDir = Vector3.RotateTowards(transform.forward, nPC.foodThatIsFound.transform.position - transform.position, nPC.agent.speed * Time.deltaTime, 0.0f);
            //if (Vector3.Distance(nPC.foodThatIsFound.transform.position, transform.position) > 1.6f)
            //    SendEvent("OUTREACH");
        }
    }
}
