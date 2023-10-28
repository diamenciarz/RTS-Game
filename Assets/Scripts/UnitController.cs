using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private bool skipLoop;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    public void GoTowards(GameObject goTo)
    {
        if(goTo == null) return;
        if(target != null) RemoveTarget();
        agent.avoidancePriority = 49;
        target = goTo;
        skipLoop = true;
    }
    void Update()
    {
        if (target == null) return;
     
        agent.SetDestination(target.transform.position);
        if (skipLoop)
        {
            skipLoop = false;
            return;
        }

        //Debug.Log("Remaining distance" + agent.remainingDistance);
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            RemoveTarget();
        }
    }
    private void RemoveTarget()
    {
        Destroy(target);
        target = null;
        agent.ResetPath();
        agent.avoidancePriority = 50;
    }
}
