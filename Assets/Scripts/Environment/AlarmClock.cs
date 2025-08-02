using System.Collections;
using TMPro;
using UnityEngine;

public class AlarmClock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMP_Text timeText;

    public bool CanInteract(Interactor interactor) => audioSource.isPlaying;
    public void Interact(Interactor interactor)
    {
        SoundPlayer.I.PlaySound("TurnOffAlarmClock", transform.position);
        audioSource.Stop();
    }

    void Start()
    {
        TimeManager.I.ScheduleFunction(new TimedCallback(8, GoOff));
    }

    void Update()
    {
        timeText.text = TimeManager.I.GetCurrentTimeString();
    }

    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(10f);
        audioSource.Stop();
    }

    void GoOff()
    {
        audioSource.Play();
        StartCoroutine(AutoOff());
    }

    public string InteractText => "Shut off";
}
