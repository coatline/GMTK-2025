using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SleepController : PlayerState
{
    const int SLEEP_TIME_MULTIPLIER = 120;

    [SerializeField] EyesCloseAnimation sleepAnimation;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] Transform characterTransform;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMP_Text sleepingZText;
    [SerializeField] GameObject sleepingUI;

    Coroutine goingToSleepCoroutine;
    Coroutine sleepingCoroutine;
    Bed bed;

    public void GetInBed(Bed bed)
    {
        this.bed = bed;

        SoundPlayer.I.PlaySound("GetInBed", transform.position);

        EnableInputs();
        NotifyActivated();

        characterTransform.position = bed.SleepPosition.position;
        goingToSleepCoroutine = StartCoroutine(GoToSleepAnimation());
    }

    IEnumerator GoToSleepAnimation()
    {
        sleepAnimation.StartFade(1f, 1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.5f));
        yield return new WaitForSeconds(1.25f);

        SetSleepState();
    }

    public void SetSleeping(Bed bed, bool setTimeModifier = false)
    {
        this.bed = bed;

        EnableInputs();
        NotifyActivated();
        characterTransform.position = bed.SleepPosition.position;

        sleepAnimation.SetAlpha(1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.01f));

        SetSleepState(setTimeModifier);
    }

    void SetSleepState(bool speedTime = true)
    {
        if (speedTime)
            TimeManager.I.SetTimeMultiplier(SLEEP_TIME_MULTIPLIER);

        sleepingUI.SetActive(true);
        audioSource.Play();
        sleepingCoroutine = StartCoroutine(SleepingAnimation());
    }

    public void GetUp()
    {
        if (goingToSleepCoroutine != null) StopCoroutine(goingToSleepCoroutine);
        if (sleepingCoroutine != null) StopCoroutine(sleepingCoroutine);

        audioSource.Stop();
        SoundPlayer.I.PlaySound("GetOutOfBed", transform.position);

        DisableInputs();
        sleepingUI.SetActive(false);
        TimeManager.I.SetTimeMultiplier(1);
        characterTransform.position = bed.WakePosition.position;
        StartCoroutine(GetUpAnimation());
    }

    IEnumerator GetUpAnimation()
    {
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward, 0.5f));
        sleepAnimation.StartFade(0f, 1f);
        yield return new WaitForSeconds(0.5f);

        SoundPlayer.I.PlaySound("Yawn", transform.position, 1, 0);
        playerController.Activate();
    }

    IEnumerator SleepingAnimation()
    {
        while (true)
            for (int i = 0; i < 25; i++)
            {
                sleepingZText.text = new string('z', i);
                yield return new WaitForSeconds(0.25f);
            }
    }

    public void OnGetUp(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            GetUp();
    }

    void EnableInputs()
    {
        playerInputs.Backed += OnGetUp;
    }

    void DisableInputs()
    {
        playerInputs.Backed -= OnGetUp;
    }

    public override void Exit() => DisableInputs();
}
