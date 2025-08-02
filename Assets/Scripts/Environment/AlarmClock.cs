using System.Collections;
using TMPro;
using UnityEngine;

public class AlarmClock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float ringDuration;
    [SerializeField] float ringHour;

    public bool CanInteract(Interactor interactor) => audioSource.isPlaying;
    public void Interact(Interactor interactor)
    {
        SoundPlayer.I.PlaySound("TurnOffAlarmClock", transform.position);
        audioSource.Stop();
    }

    void Start()
    {
        TimeManager.I.ScheduleFunction(new TimedCallback(ringHour, GoOff));
    }

    void Update()
    {
        timeText.text = TimeManager.I.GetCurrentTimeString();
    }

    IEnumerator AutoOff()
    {
        float timer = 0;

        while (timer < ringDuration)
        {
            timer += TimeManager.I.DeltaTime;
            yield return null;
        }

        audioSource.Stop();
    }

    void GoOff()
    {
        audioSource.Play();
        StartCoroutine(AutoOff());
    }

    public string InteractText => "Shut off";
}
