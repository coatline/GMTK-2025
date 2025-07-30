using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] LaptopCursor cursor;

    Interactor currentInteractor;


    public void Interact(Interactor interactor)
    {
        currentInteractor = interactor;
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    public bool CanInteract(Interactor interactor) => currentInteractor == null;
}
