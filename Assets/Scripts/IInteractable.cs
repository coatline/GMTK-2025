using UnityEngine;

public interface IInteractable
{
    string InteractText { get; }
    void Interact(Interactor interactor);
    bool CanInteract(Interactor interactor);
}
