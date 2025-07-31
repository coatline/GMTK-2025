using System.Collections;
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
    [SerializeField] GameObject sleepingUI;

    Bed bed;

    public void Activate(Bed bed)
    {
        this.bed = bed;

        EnableInputs();
        NotifyActivated();

        characterTransform.position = bed.SleepPosition.position;
        StartCoroutine(GoToSleepAnimation());
    }

    IEnumerator GoToSleepAnimation()
    {
        sleepAnimation.StartFade(1f, 1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.5f));
        yield return new WaitForSeconds(1.25f);

        TimeManager.I.SetTimeMultiplier(SLEEP_TIME_MULTIPLIER);
        sleepingUI.SetActive(true);
    }

    public void SetSleeping(Bed bed)
    {
        this.bed = bed;

        EnableInputs();
        NotifyActivated();
        characterTransform.position = bed.SleepPosition.position;

        sleepAnimation.SetAlpha(1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.01f));

        TimeManager.I.SetTimeMultiplier(SLEEP_TIME_MULTIPLIER);
        sleepingUI.SetActive(true);
    }

    public void GetUp()
    {
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

        playerController.Activate();
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
