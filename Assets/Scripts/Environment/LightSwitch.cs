using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] LightBulb lightBulb;

    public void Interact(Interactor interactor)
    {
        SoundPlayer.I.PlaySound("ToggleLight", transform.position);
        lightBulb.Toggle();
    }
    public bool CanInteract(Interactor interactor) => true;
    public string InteractText => lightBulb.On ? "Turn off" : "Turn on";
}
