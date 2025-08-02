using UnityEngine;

public class SleepThought : ParentThought
{
    [SerializeField] Bed bed;
    [SerializeField] ParentController parent;

    ParentSleepState sleepState;
    IntervalTimer sleepTimer;

    private void Start()
    {
        sleepState = new ParentSleepState(bed, parent);
        sleepTimer = new IntervalTimer(10f, true);
    }

    public override void Enter()
    {
        parent.Speaker.Say("I'm tired.");
    }

    public override void Think()
    {
        if (parent.GetDistanceFrom(bed.SleepPosition) < 1f)
        {
            parent.SetState(new ParentMoveState(bed.SleepPosition, 1f, parent/*, this, this*/));
            return;
        }

        if (parent.CurrentState != sleepState)
        {
            sleepTimer.Start();
            parent.SetState(sleepState);
        }
        else if (sleepTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
        {
            sleepTimer.Stop();
            parent.SetThought(null);
        }
    }

    public override string ActionName => "Getting some sleep.";
}
