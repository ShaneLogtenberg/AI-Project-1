using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChild : MonoBehaviour
{
    private CamFocus parentScript;
    void Start()
    {
        parentScript = transform.parent.GetComponent<CamFocus>();
    }
    void OnTriggerEnter(Collider other)
    {
        parentScript.recieveTriggerEnter(name, other);
    }

    void OnTriggerExit(Collider other)
    {
        parentScript.recieveTriggerExit(name, other);
    }
}
