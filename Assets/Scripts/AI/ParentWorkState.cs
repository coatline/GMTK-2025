using UnityEngine;
using UnityEngine.AI;

public class ParentWorkState : ParentState
{
    [SerializeField] Transform deskTarget;
    [SerializeField] float sitDistance = 0.5f;

    bool isSitting = false;
    float work;

    public override void Enter()
    {
        isSitting = false;
    }

    public override void Exit()
    {
        print("Exiting Work!");
        // Reset posture or stand up
        transform.rotation = Quaternion.identity;
        work = 0;
    }

    public override void Perform()
    {
        work += TimeManager.I.MinutesDeltaTime;

        if (work > 10)
            parentController.SwitchState(null);

        transform.rotation = Quaternion.LookRotation(deskTarget.forward);
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

    public override float MinDistance => 1f;
    public override Transform Target => deskTarget;
}
