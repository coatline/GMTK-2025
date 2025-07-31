using UnityEngine;

public class AlarmClock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioSource audioSource;


    public bool CanInteract(Interactor interactor) => audioSource.isPlaying;
    public void Interact(Interactor interactor)
    {
        audioSource.Stop();
    }
    public string InteractText => "Shut off";
}
