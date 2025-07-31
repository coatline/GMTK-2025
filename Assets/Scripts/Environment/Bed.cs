using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public string InteractText => throw new System.NotImplementedException();

    PlayerStateController playerStateController;

    public void Interact(Interactor interactor)
    {
        playerStateController = interactor.GetComponentInParent<PlayerStateController>();
        //playerStateController.GoToSleep();
        //playerStateController.CameraAnimator.Animate(new CameraCommand(cameraPosition, transform.forward, 0.25f));
    }

    public void Quit()
    {

    }

    public bool CanInteract(Interactor interactor) => true;
}
