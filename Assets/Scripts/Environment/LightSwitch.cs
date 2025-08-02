using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] LightBulb[] lightBulbs;

    public void Interact(Interactor interactor)
    {
        SoundPlayer.I.PlaySound("ToggleLight", transform.position);

        for (int i = 0; i < lightBulbs.Length; i++)
            lightBulbs[i].Toggle();
    }

    public bool CanInteract(Interactor interactor) => true;
    public string InteractText => lightBulbs[0].On ? "Turn off" : "Turn on";
}
