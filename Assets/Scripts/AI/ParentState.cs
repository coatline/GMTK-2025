using UnityEngine;

public abstract class ParentState : MonoBehaviour
{
    [SerializeField] protected ParentController parentController;

    public abstract void Perform(float deltaTime);
    public abstract void Enter();
    public abstract void Exit();
}
