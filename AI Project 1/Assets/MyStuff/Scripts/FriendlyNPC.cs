using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class FriendlyNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    Blackboard blackboard;
    float wanderSpeed;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Blackboard>();
        wanderSpeed = Random.Range(2f, 3f);
        blackboard.GetFloatVar("Wander Speed").Value = wanderSpeed;
    }
}
