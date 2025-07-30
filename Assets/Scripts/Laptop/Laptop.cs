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
        interactor.PlayerController.IsOccupied = true;
        cursor.Activate();
    }

    public void Quit()
    {
        currentInteractor.PlayerController.IsOccupied = false;
        cursor.Deactivate();
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    void Update()
    {

    }

    public bool CanInteract(Interactor interactor) => currentInteractor == null;
}
