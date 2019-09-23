using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Napping : StateBehaviour
{
    private NavMeshAgent agent;
    Transform body;
    public float napTime;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        napTime = blackboard.GetFloatVar("Nap Time").Value;
        agent.isStopped = true;
        body = transform.GetChild(0).transform;
        body.rotation *= Quaternion.Euler(new Vector3(0, 90, 0));
        body.transform.position += new Vector3(0, -0.25f, 0);
    }

    void Update()
    {
        napTime -= 1 * Time.deltaTime;

        if (napTime < 0)
        {
            gameObject.SendMessage("WakeUp");
        }
    }

    void WakeUp()
    {
        body.rotation *= Quaternion.Euler(new Vector3(0, -90, 0));
        body.transform.position += new Vector3(0, 0.25f, 0);
        gameObject.SendMessage("NotTiredAnyMore");
        blackboard.SendEvent("AWAKENED");
    }

}
