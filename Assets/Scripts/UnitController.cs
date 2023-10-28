using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public float originalStoppingDistance;
    public GameObject target;
    private NavMeshAgent agent;
    private bool skipLoop;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originalStoppingDistance = NavMesh.GetSettingsByID(agent.agentTypeID).agentRadius;
    }

    // Update is called once per frame
    public void GoTowards(GameObject goTo, float newStoppingDistance)
    {
        if(goTo == null) return;
        if(target != null) RemoveTarget();
        agent.avoidancePriority = 49;
        agent.stoppingDistance = (newStoppingDistance == -1) ? originalStoppingDistance : newStoppingDistance;
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
