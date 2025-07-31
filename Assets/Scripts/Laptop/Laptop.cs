using TMPro;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] LaptopCursor cursor;
    [SerializeField] TMP_Text moneyText;

    PlayerStateController playerStateController;


    public void Interact(Interactor interactor)
    {
        playerStateController = interactor.GetComponentInParent<PlayerStateController>();
        playerStateController.SetLaptop(this);
        playerStateController.CameraAnimator.Animate(new CameraCommand(cameraPosition, transform.forward, 0.25f));

        cursor.Activate();
    }

    public void Quit()
    {
        playerStateController.SetNormal();
        playerStateController.CameraAnimator.ResetPosition();
        playerStateController = null;

        cursor.Deactivate();
    }

    public void Purchase()
    {
        print("OKay!!");
    }

    public string InteractText => "Hop online";
    public bool CanInteract(Interactor interactor) => playerStateController == null;
}
