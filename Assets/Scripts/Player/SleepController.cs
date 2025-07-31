using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SleepController : PlayerState
{
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] EyesCloseAnimation sleepAnimation;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform characterTransform;
    [SerializeField] PlayerInputs playerInputs;

    Bed bed;

    public void Activate(Bed bed)
    {
        this.bed = bed;

        EnableInputs();
        NotifyActivated();

        characterTransform.position = bed.SleepPosition.position;

        sleepAnimation.StartFade(1f, 1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.5f));
    }

    public void SetSleeping()
    {
        sleepAnimation.StartFade(1f, 0.01f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.01f));
    }

    public void GetUp()
    {
        DisableInputs();
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
