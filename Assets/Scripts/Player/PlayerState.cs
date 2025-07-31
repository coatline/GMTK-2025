using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public event System.Action<PlayerState> Activated;

    protected void NotifyActivated()
    {
        Activated?.Invoke(this);
    }

    // This should only be called from PlayerStateController
    public abstract void Exit();
}
