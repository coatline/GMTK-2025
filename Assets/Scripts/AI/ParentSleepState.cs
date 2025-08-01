using UnityEngine;
using UnityEngine.AI;

public class ParentSleepState : ParentState
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Bed bed;

    bool isLyingDown;
    float sleep;

    public override void Enter()
    {
        isLyingDown = false;

        if (navMeshAgent != null)
            navMeshAgent.SetDestination(bed.transform.position);
    }

    public override void Perform(float deltaTime)
    {
        if (isLyingDown) return;

        if (navMeshAgent.remainingDistance <= 0.5f)
        {
            navMeshAgent.isStopped = true;
            LieDown();
        }
    }

    void LieDown()
    {
        isLyingDown = true;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        Debug.Log("Parent is lying down in bed.");
    }

    public override void Exit()
    {
        navMeshAgent.isStopped = false;
        transform.rotation = Quaternion.identity;
    }
}

//public enum StateType
//{
//    Idle,
//    Moving,
//    Working
//}