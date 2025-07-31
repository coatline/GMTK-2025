using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] LightBulb lightBulb;

    public void Interact(Interactor interactor) => lightBulb.Toggle();
    public bool CanInteract(Interactor interactor) => true;
    public string InteractText => lightBulb.On ? "Turn off" : "Turn on";
}
