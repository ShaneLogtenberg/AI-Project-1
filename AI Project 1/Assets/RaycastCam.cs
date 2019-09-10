using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectHit;
    RaycastHit hit;
    Ray ray;

    private void FixedUpdate()
    {

        ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));


        if (Physics.Raycast(ray, out hit))
        {
            objectHit = hit.transform.gameObject;
            
            Debug.Log("I'm looking at " + hit.transform.name);
        }
        else
        {
            objectHit = null;
            Debug.Log("I'm looking at nothing!");
        }
    }
}
