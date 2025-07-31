using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SleepController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] EyesCloseAnimation sleepAnimation;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform characterTransform;

    Bed bed;

    public void Activate(Bed bed)
    {
        this.bed = bed;
        playerInput.SwitchCurrentActionMap("Sleep");

        characterTransform.position = bed.SleepPosition.position;
        sleepAnimation.StartFade(1f, 1f);
        cameraAnimator.Animate(new CameraCommand(cameraAnimator.NormalPosition, -bed.transform.forward + new Vector3(0, 0.75f, 0), 0.5f));
    }

    public void GetUp()
    {
        playerInput.DeactivateInput();
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
}
