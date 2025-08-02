using UnityEngine;
using UnityEngine.AI;

public class ParentSleepState : ParentState
{
    readonly Bed bed;
    bool isLyingDown;
    float sleep;

    public ParentSleepState(Bed bed, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(parent, nextState, rootState)
    {
        this.bed = bed;
    }

    public override void Enter()
    {
        isLyingDown = true;
        parent.Animator.enabled = false;
        parent.transform.SetPositionAndRotation(bed.SleepPosition.position, Quaternion.Euler(90f, 0f, 0f));
        sleep = 0;
    }

    public override void Update()
    {
        if (parent.GetDistanceFrom(bed.SleepPosition) < 1f)
        {
            parent.SetState(new ParentMoveState(bed.SleepPosition, 1f, parent, this, this));
            return;
        }

        if (sleep > 10)
            parent.SetState(NextState);
        else
            sleep += TimeManager.I.MinutesDeltaTime;
    }

    public override void Exit()
    {
        parent.Animator.enabled = true;
        parent.transform.SetPositionAndRotation(bed.WakePosition.position, Quaternion.identity);
        Debug.Log("Exiting Sleep!");
    }

    public override string Name => "Sleeping";
}