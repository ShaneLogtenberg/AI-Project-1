using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class FriendlyNPC : AllNPC
{
    public float wanderSpeed;
    new void Start()
    {
        base.Start();
        wanderSpeed = Random.Range(2f, 3f);
        blackboard.GetFloatVar("Wander Speed").Value = wanderSpeed;
    }
}
