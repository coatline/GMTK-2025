using UnityEngine;

public class SleepThought : ParentThought
{
    [SerializeField] Bed bed;
    [SerializeField] ParentController parent;

    ParentSleepState sleepState;
    ParentMoveState moveState;
    IntervalTimer sleepTimer;

    private void Start()
    {
        moveState = new ParentMoveState(bed.SleepPosition, 1f, parent);
        sleepState = new ParentSleepState(bed, parent);
        sleepTimer = new IntervalTimer(10f, true);
    }

    public override void Enter()
    {
        parent.Speaker.Say("I'm tired.");
    }

    public override void Think()
    {
        if (parent.CurrentState != sleepState)
        {
            if (parent.GetDistanceFrom(bed.SleepPosition) > 1f)
                parent.SetState(moveState);
            else
            {
                sleepTimer.Start();
                parent.SetState(sleepState);
            }
        }
        else if (sleepTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
        {
            sleepTimer.Start();
            parent.SetThought(null);
        }
    }

    public override string ActionName => "Getting some sleep.";
}
