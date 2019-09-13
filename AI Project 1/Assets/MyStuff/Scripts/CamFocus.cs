using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class CamFocus : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectHit;
    public GameObject playerFront;


    public void recieveTriggerEnter(string fromObject, Collider other)
    {
        if (fromObject == "FocusConeCollider")
        {
            if (other.gameObject.layer != 8)
            {
                Debug.Log("TriggerEnter \"" + other.gameObject.name + " \"Object");
                other.gameObject.GetComponent<Blackboard>().SendEvent("INFOCUS");

            }
        }
        if (fromObject == "ViewConeCollider")
        {
            if (other.gameObject.layer != 8)
            {
                Debug.Log("TriggerEnter \"" + other.gameObject.name + " \"Object");
                other.gameObject.GetComponent<Blackboard>().SendEvent("ONSCREEN");

            }
        }
    }        

    void OnTriggerStay(Collider other)  
    {
        if (other.gameObject.layer != 8)
        {
            //Debug.Log("TriggerStay \"" + other.gameObject.name + " \"Object");
            other.gameObject.GetComponent<MovingtoPlayer>().UpdatePlayerPosition(playerFront.transform.position);
        }
    }

    public void recieveTriggerExit(string fromObject, Collider other)
    {
        if (fromObject == "FocusConeCollider")
        {
            if (other.gameObject.layer != 8)
            {
                Debug.Log("TriggerExit \"" + other.gameObject.name + " \"Object");
                other.gameObject.GetComponent<Blackboard>().SendEvent("OUTFOCUS");

            }
        }
        if (fromObject == "ViewConeCollider")
        {
            if (other.gameObject.layer != 8)
            {
                Debug.Log("TriggerExit \"" + other.gameObject.name + " \"Object");
                other.gameObject.GetComponent<Blackboard>().SendEvent("OFFSCREEN");

            }
        }
    }

    private void FixedUpdate()
    {
            

    }
}
