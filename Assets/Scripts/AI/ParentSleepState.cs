using UnityEngine;
using UnityEngine.AI;

public class ParentSleepState : ParentState
{
    readonly Bed bed;

    public ParentSleepState(Bed bed, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(parent, nextState, rootState)
    {
        this.bed = bed;
    }

    public override void Enter()
    {
        parent.Animator.enabled = false;
        parent.transform.SetPositionAndRotation(bed.SleepPosition.position, Quaternion.Euler(90f, 0f, 0f));
    }

    public override void Update()
    {
        // TODO: Increase sleep attribute
    }

    public override void Exit()
    {
        parent.Animator.enabled = true;
        parent.transform.SetPositionAndRotation(bed.WakePosition.position, Quaternion.identity);
        Debug.Log("Exiting Sleep!");
    }

    public override string ActionName => "Sleeping.";
}