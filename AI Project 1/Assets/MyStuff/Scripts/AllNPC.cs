using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class AllNPC : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Blackboard blackboard;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public GlobalBlackboard global;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Vision vision;
    [HideInInspector]
    public StateBehaviour state;

    public GameObject foodThatIsFound;

    public bool IsVisiableToPlayer;
    public bool HasFoundFood;

    public float wanderSpeed;
    public float napTime;
    public float awakeTime;

    [HideInInspector]
    public float maxAwakeTime;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        blackboard = GetComponent<Blackboard>();
        state = GetComponent<StateBehaviour>();
        global = GameObject.Find("GlobalBlackboard").GetComponent<GlobalBlackboard>();
        player = global.GetGameObjectVar("Player");
        vision = transform.GetComponentInChildren<Vision>();
        //InvokeRepeating("Sniff",1f,1f);
        maxAwakeTime = awakeTime;
    }

    void OnVisionEnter()
    {
        if(foodThatIsFound == null)
        {
            foodThatIsFound = vision.visibleObjects[0];
            HasFoundFood = true;
        }
    }

    void OnVisionExit()
    {
        if (foodThatIsFound == null)
        {
            HasFoundFood = false;
        }
    }

    //void Sniff()
    //{
    //    if(GameObject.FindGameObjectWithTag("Food") !=null && foodThatIsFound == null)
    //    {
    //        foodThatIsFound = GameObject.FindGameObjectWithTag("Food");
    //        HasFoundFood = true;
    //    }

    //    if (foodThatIsFound == null)
    //    {
    //        HasFoundFood = false;
    //    }
    //}
    void Update()
    {
        if (awakeTime >= 0)
        {
            awakeTime -= 1 * Time.deltaTime;
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
