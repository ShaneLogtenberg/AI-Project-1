﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class ShyNPC : AllNPC
{
    public Vector3[] hiddingSpots;
    public GameObject hiddingSpotsHolder;
    new void Start()
    {
        base.Start();
        hiddingSpots = new Vector3[hiddingSpotsHolder.transform.childCount];
        for(int i =0; i < hiddingSpots.Length; i++)
        {
            hiddingSpots[i] = hiddingSpotsHolder.transform.GetChild(i).transform.position;
        }
    }

    IEnumerator Think()
    {
        while (true)
        {
            if (HasFoundFood)
            {
                if (foodThatIsFound.transform.parent != null)
                {
                    //Stay Hidden
                }
                else if (!player.GetComponent<Vision>().PointInSight(NavMeshLocation(foodThatIsFound.transform.position)))
                {
                    if (state.stateName != "MovingToFood" && state.stateName != "Eating")
                    {
                        blackboard.SendEvent("HUNGRY");
                    }
                }
            }        

            if (IsVisiableToPlayer && !HasFoundFood)
            {
                if (state.stateName != "Retreating")
                {
                    blackboard.SendEvent("INFOCUS");
                }
            }

            if (!HasFoundFood && !IsVisiableToPlayer)
            {
                if (state.stateName != "Hidding")
                {
                    blackboard.SendEvent("OUTFOCUS");
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
