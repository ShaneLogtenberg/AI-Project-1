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
    public GlobalBlackboard global;
    public GameObject foodThatIsFound;
    public StateBehaviour state;

    public bool IsVisiableToPlayer;
    public bool HasFoundFood;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        blackboard = GetComponent<Blackboard>();
        state = GetComponent<StateBehaviour>();
        InvokeRepeating("Sniff",1f,1f);
    }

    void Sniff()
    {
        Debug.Log("sniff");
        if(GameObject.FindGameObjectsWithTag("Food") !=null && foodThatIsFound == null)
        {
            foodThatIsFound = GameObject.FindGameObjectWithTag("Food");
            HasFoundFood = true;
        }
        else if(foodThatIsFound == null)
        {
            HasFoundFood = false;
        }
    }

    public Vector3 NavMeshLocation(Vector3 point)
    {
        NavMeshHit navHit;
        Vector3 navPoint = Vector3.zero;
        if(NavMesh.SamplePosition(point,out navHit, 1, NavMesh.AllAreas))
        {
            navPoint = navHit.position;
        }
        return navPoint;
    }
}
