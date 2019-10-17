using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using UnityEngine.AI;

public class Eating : StateBehaviour
{
    AllNPC nPC;

    public void OnEnable()
    {
        nPC = GetComponent<AllNPC>();
        Invoke("Eat",0.01f);
    }

    private void Eat()
    {
        nPC.agent.isStopped = true;
        nPC.animator.SetBool("Walk",false);
        if(nPC.foodThatIsFound != null)
            nPC.foodThatIsFound.GetComponent<Rigidbody>().velocity = Vector3.zero;
        nPC.animator.SetTrigger("Eat");
    }

    public void Finished()
    {
        Destroy(nPC.foodThatIsFound);
        nPC.HasFoundFood = false;

        if(GetComponent<FriendlyNPC>()!= null)
        {
            GetComponent<FriendlyNPC>().Fed();            
        }
        else
        {
            SendEvent("NOFOOD");
        }
    }

    // Called when the state is disabled
    void OnDisable()
    {
        nPC.agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nPC.foodThatIsFound != null)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, nPC.foodThatIsFound.transform.position - transform.position, nPC.agent.speed * Time.deltaTime, 0.0f);
            gameObject.transform.rotation = Quaternion.LookRotation(newDir);

            if (Vector3.Distance(nPC.foodThatIsFound.transform.position, transform.position) > 1f)
            {
                SendEvent("OUTREACH");
            }
        }
        else
        {
            SendEvent("NOFOOD");
        }
    }
}
