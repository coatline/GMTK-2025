using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField] Transform wakePosition;
    [SerializeField] Transform sleepPosition;

    public void Interact(Interactor interactor)
    {
        interactor.GetComponentInParent<PlayerStateController>().SleepController.Activate(this);
    }

    public Transform WakePosition => wakePosition;
    public Transform SleepPosition => sleepPosition;
    public bool CanInteract(Interactor interactor) => true;
    public string InteractText => "Sleep";
}
