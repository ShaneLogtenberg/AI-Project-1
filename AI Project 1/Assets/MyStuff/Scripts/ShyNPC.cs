using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class ShyNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 hiddingSpot;
    Blackboard blackboard;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hiddingSpot = transform.position;
        blackboard = GetComponent<Blackboard>();
        blackboard.GetVector3Var("Point1").Value = hiddingSpot;
    }
}
