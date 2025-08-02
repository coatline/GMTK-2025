using UnityEngine;

public abstract class ParentThought : MonoBehaviour
{

    // Want to sleep
    // Want to eat (get ingredients, turn on stove, cook, plate, eat)
    // Want to talk to player (find player, talk to him (if he moves, tell him to come back, find player))
    // 

    public abstract void Think();
    public virtual void Enter() { }
    public virtual void Exit() { }

    public string TypeName => GetType().Name;
    public abstract string ActionName { get; }
}
