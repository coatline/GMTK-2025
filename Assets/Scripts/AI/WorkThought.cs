using UnityEngine;

public class WorkThought : ParentThought
{
    [SerializeField] Transform workPosition;
    [SerializeField] ParentController parent;

    ParentMoveState moveState;
    ParentSitState sitState;
    IntervalTimer workTimer;

    private void Start()
    {
        moveState = new ParentMoveState(workPosition, 1f, parent);
        sitState = new ParentSitState(workPosition, parent);
        workTimer = new IntervalTimer(10f, true);
    }

    public override void Enter()
    {
        parent.Speaker.Say("I need to work.");
    }

    public override void Think()
    {
        // If we aren't already sitting working, see if we can get there.
        if (parent.CurrentState != sitState)
        {
            if (parent.GetDistanceFrom(workPosition) > 1f)
                parent.SetState(moveState);
            else
            {
                workTimer.Start();
                parent.SetState(sitState);
            }
        }
        else if (workTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
        {
            workTimer.Start();
            parent.SetThought(null);
        }
    }

    public override string ActionName => "Getting some work done.";
}
