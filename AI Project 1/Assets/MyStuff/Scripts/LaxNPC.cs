using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class LaxNPC : AllNPC
{
    new void Start()
    {
        base.Start();
        wanderSpeed = Random.Range(1f, 2f);
        napTime += Random.Range(-3f, 3f);
        StartCoroutine(Think());
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
    //        if (collision.gameObject.layer == 9)

    IEnumerator Think()
    {
        while (true)
        {
            if (HasFoundFood && foodThatIsFound != null)
            {

                if (foodThatIsFound.transform.parent == player.transform)
                {
                    //if (state.stateName != "MovingtoPlayer" && state.stateName != "ReachingPlayer")
                    //{
                    blackboard.SendEvent("INFOCUS");
                    //}
                }
                else
                //if (state.stateName != "MovingtoFood" && state.stateName != "Eating")
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

            yield return new WaitForSeconds(2f);
        }
    }
}
