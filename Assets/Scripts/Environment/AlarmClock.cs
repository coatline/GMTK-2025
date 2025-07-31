using TMPro;
using UnityEngine;

public class AlarmClock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMP_Text timeText;

    public bool CanInteract(Interactor interactor) => audioSource.isPlaying;
    public void Interact(Interactor interactor)
    {
        audioSource.Stop();
    }

    void Update()
    {
        timeText.text = TimeManager.I.GetCurrentTimeString();
    }

    public string InteractText => "Shut off";
}
