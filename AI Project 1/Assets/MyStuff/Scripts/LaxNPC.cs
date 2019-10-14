using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class LaxNPC : AllNPC
{
    public float wanderSpeed;
    public float napTime;
    public float awakeTime;
    float maxAwakeTime;
    new void Start()
    {
        base.Start();
        wanderSpeed = Random.Range(1f, 2f);
        //napTime = Random.Range(3f, 5f);
        maxAwakeTime = awakeTime;
        blackboard.GetFloatVar("Wander Speed").Value = wanderSpeed;
        blackboard.GetFloatVar("Nap Time").Value = napTime;
        StartCoroutine(Think());
    }

    public void NotTiredAnyMore()
    {
        awakeTime = maxAwakeTime;
    }
    void Update()
    {
        if (awakeTime >= 0)
        {
            awakeTime -= 1 * Time.deltaTime;
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (agent.speed >= 5)
    //    {
    //        if (collision.gameObject.layer == 9)aaaaaaaaaa

    IEnumerator Think()
    {
        while (true)
        {


            if (HasFoundFood)
            {
                if (foodThatIsFound.transform.parent != null)
                {
                    if (state.stateName != "MovingToPlayer"  && state.stateName != "ReachingPlayer")
                    {
                        blackboard.SendEvent("INFOCUS");
                    }
                }
                else if (state.stateName != "MovingToFood" && state.stateName != "Eating")
                {
                        blackboard.SendEvent("HUNGRY");
                }
                
            }
            else if (awakeTime <= 0)
            {
                awakeTime = 0;
                blackboard.SendEvent("TIRED");
            }
            else if (state.stateName != "Wandering")
            {
                blackboard.SendEvent("NOFOOD");
            }

            //if (IsVisiableToPlayer && !HasFoundFood)
            //{
            //    if (state.stateName != "Retreating")
            //    {
            //        blackboard.SendEvent("INFOCUS");
            //    }
            //}

            //if (!HasFoundFood && !IsVisiableToPlayer)
            //{
            //    if (state.stateName != "Hidding")
            //    {
            //        blackboard.SendEvent("OUTFOCUS");
            //    }
            //}

            yield return new WaitForSeconds(2f);
        }
    }
}
