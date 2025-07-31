using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();

    public abstract string ActionMap { get; }
}
