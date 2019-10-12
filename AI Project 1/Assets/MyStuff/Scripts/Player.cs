using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using System;

public class Player : MonoBehaviour
{
    public GlobalBlackboard global;
    public GameObject VisionCollider;
    public Vision vision;
    public GameObject foodPrefab;
    private GameObject foodInHand;
    private bool canSpawnFood;
    public float foodDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        canSpawnFood = true;
    }

    void OnVisionEnter()
    {
        for(int i=0; i < vision.visibleObjects.Count; i++)
        {
            if (vision.visibleObjects[i].GetComponent<AllNPC>().IsVisiableToPlayer == false)
            {
                vision.visibleObjects[i].GetComponent<AllNPC>().IsVisiableToPlayer = true;
            }
        }
    }

    void OnVisionExit()
    {
        for (int i = 0; i < vision.visibleObjects.Count; i++)
        {
            if (vision.visibleObjects[i].GetComponent<AllNPC>().IsVisiableToPlayer == true)
            {
                vision.visibleObjects[i].GetComponent<AllNPC>().IsVisiableToPlayer = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SpawnFood();
        }
        else
        {
            DropFood();
        }
    }

    private void DropFood()
    {
        if (foodInHand != null)
        {
            foodInHand.GetComponent<Rigidbody>().isKinematic = false;
            foodInHand.transform.parent = null;
            foodInHand = null;
        }
    }

    private void SpawnFood()
    {
        if(canSpawnFood && foodInHand == null)
        {
            foodInHand = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            foodInHand.transform.parent = gameObject.transform;
            foodInHand.GetComponent<Rigidbody>().isKinematic = true;
            foodInHand.transform.localPosition = Vector3.forward;
            StartCoroutine(FoodTimer());
        }
    }

    IEnumerator FoodTimer()
    {
        canSpawnFood = false;
        yield return new WaitForSeconds(foodDelay);
        canSpawnFood = true;
        yield break;
    }
}
