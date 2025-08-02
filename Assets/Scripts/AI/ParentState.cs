using UnityEngine;

public abstract class ParentState
{
    public ParentState NextState { get; protected set; }
    public ParentState RootState { get; private set; }

    protected ParentController parent;

    protected ParentState(ParentController parent, ParentState nextState = null, ParentState rootState = null)
    {
        if (rootState == null)
            rootState = this;

        this.parent = parent;
        RootState = rootState;
        NextState = nextState;
    }

    public abstract void Update();
    public virtual void Enter() { }
    public virtual void Exit() { }

    public abstract string Name { get; }
}
