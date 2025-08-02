using UnityEngine;

public abstract class ParentState : MonoBehaviour
{
    [SerializeField] protected ParentController parentController;

    public abstract void Perform();
    public abstract void Enter();
    public abstract void Exit();

    public abstract float MinDistance { get; }
    public abstract Transform Target { get; }
}
