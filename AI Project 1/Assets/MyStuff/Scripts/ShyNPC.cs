﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class ShyNPC : AllNPC
{
    public GameObject[] hiddingSpots;
    public GameObject hiddingSpotsHolder;
    public Vision playerVision;

    new void Start()
    {
        base.Start();
        playerVision = player.GetComponent<Player>().vision;
        hiddingSpots = new GameObject[hiddingSpotsHolder.transform.childCount];
        for(int i =0; i < hiddingSpots.Length; i++)
        {
            hiddingSpots[i] = hiddingSpotsHolder.transform.GetChild(i).gameObject;
        }
        StartCoroutine(Think());
    }

    IEnumerator Think()
    {
        while (true)
        {
                if (HasFoundFood && foodThatIsFound !=null)
                {
                    if (foodThatIsFound.transform.parent != null)
                    {
                        //Stay Hidden
                    }
                    else if (!playerVision.PointInSight(foodThatIsFound))
                    {
                        //if (state.stateName != "MovingToFood" && state.stateName != "Eating")
                        //{
                        blackboard.SendEvent("HUNGRY");
                        //}
                    }
                }

                if (IsVisiableToPlayer)
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