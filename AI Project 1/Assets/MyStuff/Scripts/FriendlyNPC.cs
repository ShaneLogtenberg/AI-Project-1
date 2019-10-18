using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class FriendlyNPC : AllNPC
{
    public float maxHungerMeter = 10;
    public float hungerMeter = 1;
    public bool isHungry = false;

    new void Start()
    {
        base.Start();
        wanderSpeed = Random.Range(2f, 3f);
        StartCoroutine(Think());
    }
    void Update()
    {
        if (!isHungry)
        {
            hungerMeter = hungerMeter - 1 * Time.deltaTime;
        }

        if (hungerMeter < 0 && !isHungry)
        {
            hungerMeter = 0;
            isHungry = true;            
        }
    }

    public void Fed()
    {
        blackboard.SendEvent("TIRED");
    }

    public void ResetHunger()
    {
        hungerMeter = maxHungerMeter;
        isHungry = false;
    }


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

            if (IsVisiableToPlayer && !HasFoundFood && isHungry)
            {
                //if (state.stateName != "MovingtoPlayer" && state.stateName != "ReachingPlayer")
                //{
                    blackboard.SendEvent("INFOCUS");
                //}
            }

            if (!HasFoundFood && !IsVisiableToPlayer && !isHungry)
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
