using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class AllNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Blackboard blackboard;
    public Animator animator;
    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        blackboard = GetComponent<Blackboard>();
    }

}
