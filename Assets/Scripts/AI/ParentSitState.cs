using UnityEngine;
using UnityEngine.AI;

public class ParentSitState : ParentState
{
    readonly Transform desk;

    public ParentSitState(Transform desk, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(parent, nextState, rootState)
    {
        this.desk = desk;
    }

    public override void Enter()
    {
        parent.transform.rotation = Quaternion.LookRotation(desk.forward);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        // Reset posture or stand up

    }

    public override string StateName => "Sit";
}
