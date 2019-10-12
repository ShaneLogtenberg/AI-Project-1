using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class ReachingPlayer : StateBehaviour
{
    AllNPC nPC;
    Vector3 playerFront;
    void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        nPC.animator.SetBool("Walk", false);
    }
    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *State*");
    }

    // Update is called once per frame
    void Update()
    {
        //playerFront = GetComponent<MovingtoPlayer>().playerFront;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, playerFront-transform.position, agent.speed * Time.deltaTime, 0.0f);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, Vector3.zero - transform.position, nPC.agent.speed * Time.deltaTime, 0.0f);
        gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        nPC.agent.destination = blackboard.GetVector3Var("Point1").Value;
    }
}


