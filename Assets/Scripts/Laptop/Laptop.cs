using TMPro;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] LaptopCursor cursor;
    [SerializeField] TMP_Text moneyText;

    PlayerController playerController;


    public void Interact(Interactor interactor)
    {
        playerController = interactor.GetComponentInParent<PlayerController>();
        playerController.SetState(PlayerState.Laptop);
        playerController..CameraAnimator.Animate(new CameraCommand(cameraPosition, transform.forward, 0.25f));

        cursor.Activate();
    }

    public void Quit()
    {
        currentInteractor.FocusState.SetState(CharacterState.None, this);
        currentInteractor.FocusState.CameraAnimator.ResetPosition();

        currentInteractor = null;
        cursor.Deactivate();
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    public string InteractText => "Hop online";
    public bool CanInteract(Interactor interactor) => currentInteractor == null;
}
