using UnityEngine;

public class PoopThought : ParentThought
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] ParentController parent;
    [SerializeField] Transform toilet;

    ParentMoveState moveState;
    ParentPoopState poopState;
    IntervalTimer crapTimer;

    private void Start()
    {
        poopState = new ParentPoopState(audioSource, toilet, parent);
        moveState = new ParentMoveState(toilet, 1f, parent);
        crapTimer = new IntervalTimer(30f, true);
    }

    private void ParentView_SeePlayer()
    {
        if (parent.CurrentState == poopState)
            parent.Speaker.Say("Can't you see I'm crapping here!?");
    }

    public override void Think()
    {
        // If the toilet is not available, go to the door and say hurry up!

        if (parent.CurrentState != poopState)
        {
            if (parent.GetDistanceFrom(toilet) > 1f)
                parent.SetState(moveState);
            else
            {
                crapTimer.Start();
                parent.SetState(poopState);
            }
        }
        else if (crapTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
        {
            crapTimer.Start();
            parent.SetThought(null);
        }
    }

    public override void Enter()
    {
        parent.ParentView.SeePlayer += ParentView_SeePlayer;
    }

    public override void Exit()
    {
        parent.ParentView.SeePlayer -= ParentView_SeePlayer;
    }

    public override string ActionName => "Going to the bathroom.";
}
