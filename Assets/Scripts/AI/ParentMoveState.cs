using UnityEngine;

public class ParentMoveState : ParentState
{
    readonly float minDistance;
    readonly Transform target;

    public ParentMoveState(Transform target, float minDistance, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(parent, nextState, rootState)
    {
        this.target = target;
        this.minDistance = minDistance;
    }

    public override void Enter()
    {
        parent.Nav.enabled = true;
        parent.Nav.isStopped = false;
    }

    public override void Update()
    {
        if (target == null)
        {
            parent.SetState(NextState);
            return;
        }

        // Are we in the right position?
        if (parent.GetDistanceFrom(target) <= minDistance)
            parent.SetState(NextState);
        else
            parent.Nav.SetDestination(target.position);

        //if (navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        //{
        //    print($"NO PATH! {navMeshAgent.pathStatus}");
        //    //currentState.Exit();
        //    //SwitchState(null);
        //}
    }

    public override void Exit()
    {
        parent.Nav.isStopped = true;
        parent.Nav.enabled = false;
    }

    public override string ActionName => $"Moving to {target.name}";
}
