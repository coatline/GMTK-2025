using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] FocusState focusState;
    [SerializeField] ObjectHolder objectHolder;
    public ObjectHolder ObjectHolder => objectHolder;
    public FocusState FocusState => focusState;

    protected IInteractable currentTarget;

    public void TryInteract()
    {
        if (currentTarget != null && currentTarget.CanInteract(this))
            currentTarget.Interact(this);

        currentTarget = null;
    }
}
