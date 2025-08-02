using UnityEngine;

public class WorkThought : ParentThought
{
    [SerializeField] Transform workPosition;
    [SerializeField] ParentController parent;

    ParentSitState sitState;
    IntervalTimer workTimer;

    private void Start()
    {
        sitState = new ParentSitState(workPosition, parent);
        workTimer = new IntervalTimer(10f, true);
    }

    public override void Enter()
    {
        parent.Speaker.Say("I'm tired.");
    }

    public override void Think()
    {
        if (parent.GetDistanceFrom(workPosition) < 1f)
        {
            parent.SetState(new ParentMoveState(workPosition, 1f, parent));
            return;
        }

        if (parent.CurrentState != sitState)
        {
            workTimer.Start();
            parent.SetState(sitState);
        }
        else if (workTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
        {
            workTimer.Stop();
            parent.SetThought(null);
        }
    }

    public override string ActionName => "Getting some work done.";
}
