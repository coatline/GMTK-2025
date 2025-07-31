using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaptopController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] PlayerInput playerInput;

    Laptop laptop;

    public void Activate(Laptop laptop)
    {
        this.laptop = laptop;
        playerInput.SwitchCurrentActionMap("Laptop");
        cameraAnimator.Animate(laptop.LaptopCameraCommand);
    }

    public void Deactivate()
    {
        playerInput.DeactivateInput();
        StartCoroutine(ResetCameraAndExit());
    }

    IEnumerator ResetCameraAndExit()
    {
        CameraCommand command = new CameraCommand(cameraAnimator.CameraNormalPosition, cameraAnimator.transform.forward, 0.5f);
        cameraAnimator.Animate(command);
        bool cameraDone = false;
        command.Finished += () => cameraDone = true;

        yield return new WaitUntil(() => cameraDone);
        playerController.Activate();
        laptop = null;
        //playerStateController.SwitchToState(playerStateController.PlayerController);
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
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
            laptop.LaptopCursor.Move(ctx.ReadValue<Vector2>() * Time.deltaTime);
        else
            laptop.LaptopCursor.Move(ctx.ReadValue<Vector2>() * Time.deltaTime);
    }

    //public override string ActionMap => "Laptop";
}
