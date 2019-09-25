using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class ShyNPC : AllNPC
{
    public Vector3 hiddingSpot;
    new void Start()
    {
        base.Start();
        hiddingSpot = transform.position;
        blackboard.GetVector3Var("Point1").Value = hiddingSpot;
    }
}
