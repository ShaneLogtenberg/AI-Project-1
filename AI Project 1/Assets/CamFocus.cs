using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class CamFocus : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectHit;
    public ConeCollider viewCone;
    public GameObject playerFront;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            Debug.Log("TriggerEnter \"" + other.gameObject.name + " \"Object");
            other.gameObject.GetComponent<Blackboard>().SendEvent("INFOCUS");
            
        }
    }

    void OnTriggerStay(Collider other)  
    {
        if (other.gameObject.layer != 8)
            Debug.Log("TriggerStay \"" + other.gameObject.name + " \"Object");
        other.gameObject.GetComponent<MovingtoPlayer>().UpdatePlayerPosition(playerFront.transform.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            Debug.Log("TriggerExit \"" + other.gameObject.name + " \"Object");
            other.gameObject.GetComponent<Blackboard>().SendEvent("OUTFOCUS");
        }
    }

    private void FixedUpdate()
    {
            

    }
}
