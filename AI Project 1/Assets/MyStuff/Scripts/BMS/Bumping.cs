using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Bumping : StateBehaviour
{
    AllNPC nPC;
    private void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
    }
}
