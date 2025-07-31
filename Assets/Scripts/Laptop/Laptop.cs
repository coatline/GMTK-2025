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
        //interactor.state = true;
        cursor.Activate();
    }

    public void Quit()
    {
        //currentInteractor.IsOccupied = false;
        cursor.Deactivate();
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    void Update()
    {

    }

    public string InteractText => "Hop online";
    public bool CanInteract(Interactor interactor) => currentInteractor == null;
}
