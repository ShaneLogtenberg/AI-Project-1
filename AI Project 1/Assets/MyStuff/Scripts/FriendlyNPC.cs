using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class FriendlyNPC : AllNPC
{
    public float wanderSpeed;
    public float remainingDistance;
    public bool navmeshpending;
    public float maxHungerMeter = 10;
    public float hungerMeter = 1;
    public bool hangry = false;
    new void Start()
    {
        base.Start();
        wanderSpeed = Random.Range(2f, 3f);
        blackboard.GetFloatVar("Wander Speed").Value = wanderSpeed;
        StartCoroutine(Think());
    }
    void Update()
    {
        if (!hangry)
        {
            hungerMeter = hungerMeter - 1 * Time.deltaTime;
        }

        if (hungerMeter < 0)
        {
            hangry = true;            
        }
    }

    public void Fed()
    {
        hungerMeter = maxHungerMeter;
        hangry = false;
    }

    IEnumerator Think()
    {
        while (true)
        {
            if (HasFoundFood && hangry)
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

            if (IsVisiableToPlayer && !HasFoundFood && hangry)
            {
                //if (state.stateName != "MovingtoPlayer" && state.stateName != "ReachingPlayer")
                //{
                    blackboard.SendEvent("INFOCUS");
                //}
            }

            if (!HasFoundFood && !IsVisiableToPlayer && !hangry)
            {
                if (state.stateName != "Wandering")
                {
                    blackboard.SendEvent("OUTFOCUS");
                }
            }           

            yield return new WaitForSeconds(2f);
        }
    }
}
