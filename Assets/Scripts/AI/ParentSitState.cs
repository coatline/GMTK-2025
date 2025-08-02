using UnityEngine;
using UnityEngine.AI;

public class ParentSitState : ParentState
{
    readonly Transform chair;

    public ParentSitState(Transform chair, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(parent, nextState, rootState)
    {
        this.chair = chair;
    }

    public override void Enter()
    {
        parent.transform.rotation = Quaternion.LookRotation(chair.forward);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        // Reset posture or stand up

    }

    public override string ActionName => "Sitting.";
}
