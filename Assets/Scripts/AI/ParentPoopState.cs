using UnityEngine;

public class ParentPoopState : ParentSitState
{
    readonly AudioSource audioSource;
    public ParentPoopState(AudioSource audioSource, Transform chair, ParentController parent, ParentState nextState = null, ParentState rootState = null) : base(chair, parent, nextState, rootState)
    {
        this.audioSource = audioSource;
    }

    public override void Enter()
    {
        base.Enter();
        audioSource.Play();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        audioSource.Stop();
    }

    public override string ActionName => "Pooping.";
}
