using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class ReachingPlayer : StateBehaviour
{
    AllNPC nPC;
    Vector3 playerFront;
    Vector3 playerPosition;
    bool waiting;
    void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        nPC.agent.isStopped = true;
        nPC.animator.SetBool("Walk", false);
        nPC.animator.SetTrigger("Meow");
        InvokeRepeating("UpdatePlayerPosition", 0, 1f);
    }
    // Called when the state is disabled

    public void UpdatePlayerPosition()
    {
        GameObject player = nPC.global.GetGameObjectVar("Player");
        playerFront = player.transform.GetChild(3).transform.position;
        playerPosition = player.transform.position;
    }
    void OnDisable()
    {
        CancelInvoke("UpdatePlayerPosition");
        nPC.agent.isStopped = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        CancelInvoke("UpdatePlayerPosition");
        SendEvent("OUTFOCUS");
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        //playerFront = GetComponent<MovingtoPlayer>().playerFront;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, playerFront-transform.position, agent.speed * Time.deltaTime, 0.0f);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, playerPosition - transform.position, nPC.agent.speed * Time.deltaTime, 0.0f);
        gameObject.transform.rotation = Quaternion.LookRotation(newDir);

        if (Vector3.Distance(playerFront, transform.position) > 1f)
        {
            CancelInvoke("UpdatePlayerPosition");
            SendEvent("OUTREACH");
        }
        if (!nPC.IsVisiableToPlayer)
        {
            StartCoroutine(Wait());
        } else
        {
            StopCoroutine(Wait());
        }
               
    }
}


