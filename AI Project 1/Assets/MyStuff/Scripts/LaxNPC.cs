using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class LaxNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    Blackboard blackboard;
    float wanderSpeed;
    public float napTime;
    public float awakeTime;
    float maxAwakeTime;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Blackboard>();
        wanderSpeed = Random.Range(1f, 2f);
        //napTime = Random.Range(3f, 5f);
        maxAwakeTime = awakeTime;
        blackboard.GetFloatVar("Wander Speed").Value = wanderSpeed;
        blackboard.GetFloatVar("Nap Time").Value = napTime;
        InvokeRepeating("CheckTired", 0f, 2f);
    }

    void CheckTired()
    {
        if (awakeTime < 0)
        {
            blackboard.SendEvent("TIRED");
            awakeTime = 0;
        }
    }
    void NotTiredAnyMore()
    {
        awakeTime = maxAwakeTime;
    }
    void Update()
    {
        awakeTime -= 1 * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (agent.speed >= 5)
        {
            if (collision.gameObject.layer == 9)
            {
                blackboard.SendEvent("BUMP");
                collision.gameObject.GetComponent<Blackboard>().SendEvent("BUMP");
            }
        }
        else
        {
            blackboard.SendEvent("BUMP");
        }
    }
}
