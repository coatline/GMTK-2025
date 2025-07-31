using TMPro;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] LaptopCursor cursor;
    [SerializeField] TMP_Text moneyText;

    Interactor currentInteractor;


    public void Interact(Interactor interactor)
    {
        currentInteractor = interactor;
        currentInteractor.FocusState.SetState(CharacterState.Laptop);
        cursor.Activate();
    }

    public void Quit()
    {
        currentInteractor.FocusState.SetState(CharacterState.None);
        cursor.Deactivate();
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    public string InteractText => "Hop online";
    public bool CanInteract(Interactor interactor) => currentInteractor == null;
}
