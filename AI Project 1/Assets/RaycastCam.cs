using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class RaycastCam : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectHit;
    public ConeCollider viewCone;
    RaycastHit hit;
    Ray ray;

    private void FixedUpdate()
    {

        //ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));


        //if (Physics.Raycast(ray, out hit))
        //{
        //    objectHit = hit.transform.gameObject;
            
        //    Debug.Log("I'm looking at " + hit.transform.name);
        //}
        //else
        //{
        //    objectHit = null;
        //    Debug.Log("I'm looking at nothing!");
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8)
            Debug.Log("TriggerEnter \"" + other.gameObject.name + " \"Object");
            other.gameObject.GetComponent<Blackboard>().SendEvent("INFOCUS");
    }

    void OnTriggerStay(Collider other)  
    {
        if (other.gameObject.layer != 8)
            Debug.Log("TriggerStay \"" + other.gameObject.name + " \"Object");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8)
            Debug.Log("TriggerExit \"" + other.gameObject.name + " \"Object");
    }
}
