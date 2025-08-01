using UnityEngine;
using UnityEngine.AI;

public class ParentWorkState : ParentState
{
    [SerializeField] Transform deskTarget;
    [SerializeField] float sitDistance = 0.5f;
    [SerializeField] NavMeshAgent agent;

    bool isSitting = false;

    public override void Enter()
    {
        isSitting = false;

        if (deskTarget != null && agent != null)
        {
            agent.SetDestination(deskTarget.position);
        }
    }

    public override void Exit()
    {
        if (agent != null) agent.isStopped = false;

        // Reset posture or stand up
        transform.rotation = Quaternion.identity;
    }

    public override void Perform(float deltaTime)
    {
        if (isSitting) return;

        if (agent.remainingDistance <= sitDistance)
        {
            agent.isStopped = true;
            transform.rotation = Quaternion.LookRotation(deskTarget.forward);
        }
    }

    //void SitAtDesk()
    //{
    //    isSitting = true;

    //    // Face the desk (or match its rotation)
    //    transform.rotation = Quaternion.LookRotation(deskTarget.forward);
    //    Debug.Log("Parent is now working at the desk.");
    //}

    //void GetUp()
    //{

    //}
}
