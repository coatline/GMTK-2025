using TMPro;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] LaptopCursor laptopCursor;
    [SerializeField] TMP_Text moneyText;

    PlayerStateController playerStateController;

    public LaptopCursor LaptopCursor => laptopCursor;

    public void Interact(Interactor interactor)
    {
        playerStateController = interactor.GetComponentInParent<PlayerStateController>();
        playerStateController.LaptopController.Activate(this);

        laptopCursor.enabled = true;
    }

    public void Quit()
    {
        playerStateController.LaptopController.Deactivate();
        playerStateController = null;

        laptopCursor.enabled = false;
    }

    public void Purchase()
    {

    }

    public string InteractText => "Hop online";
    public bool CanInteract(Interactor interactor) => playerStateController == null;
    public CameraCommand LaptopCameraCommand => new CameraCommand(cameraPosition, transform.forward, 0.25f);
}
