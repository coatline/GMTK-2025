using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaptopController : PlayerState
{
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] PlayerInputs playerInputs;

    Laptop laptop;

    public void Activate(Laptop laptop)
    {
        NotifyActivated();
        this.laptop = laptop;
        EnableInput();
        cameraAnimator.Animate(laptop.LaptopCameraCommand);
    }

    public void Deactivate()
    {
        DisableInput();
        StartCoroutine(ResetCameraAndExit());
    }

    IEnumerator ResetCameraAndExit()
    {
        CameraCommand command = new CameraCommand(cameraAnimator.NormalPosition, cameraAnimator.transform.forward, 0.5f);
        cameraAnimator.Animate(command);

        yield return new WaitForSeconds(0.5f);

        playerController.Activate();
        laptop = null;
    }

    public void OnBack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            laptop.Quit();
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            laptop.LaptopCursor.SetMouseDown();
    }

    public void OnMoveCursor(InputAction.CallbackContext ctx)
    {
        //if (currentControlScheme == "Keyboard&Mouse")
        laptop.LaptopCursor.Move(ctx.ReadValue<Vector2>() * Time.deltaTime);
        //else
        //    laptop.LaptopCursor.Move(ctx.ReadValue<Vector2>() * Time.deltaTime);
    }

    void EnableInput()
    {
        playerInputs.Backed += OnBack;
        playerInputs.Clicked += OnClick;
        playerInputs.MovedCursor += OnMoveCursor;
    }

    void DisableInput()
    {
        playerInputs.Backed -= OnBack;
        playerInputs.Clicked -= OnClick;
        playerInputs.MovedCursor -= OnMoveCursor;
    }

    public override void Exit() => DisableInput();
}
