using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const float USE_DEADZONE = .175f;

    [Header("References")]
    [SerializeField] CharacterController characterController;
    [SerializeField] FirstPersonCamera firstPersonCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] ObjectHolder objectHolder;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Interactor interactor;
    [SerializeField] Jumper jumper;

    bool usingObject;
    Vector2 lookInputs;

    public void Enable()
    {
        firstPersonCamera.SetCurrentLookingPosition(new Vector2(firstPersonCamera.transform.eulerAngles.y, 0));
        playerInput.enabled = true;
        enabled = true;
    }

    public void Disable()
    {
        playerInput.enabled = false;
        enabled = false;
    }

    public void OnUse(InputAction.CallbackContext ctx)
    {
        float val = ctx.ReadValue<float>();

        if (val >= USE_DEADZONE && ctx.performed)
        {
            if (objectHolder.HasItem)
            {
                objectHolder.StartUsing();
                usingObject = true;
            }
        }
        else if (ctx.canceled || val < USE_DEADZONE)
        {
            if (objectHolder.HasItem)
                if (usingObject)
                    objectHolder.FinishUsing();

            usingObject = false;
        }
    }

    public void OnDrop(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            if (objectHolder.HasItem)
                objectHolder.TryDrop();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            jumper.TryJump();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            interactor.TryInteract();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        playerMovement.SetMoveInput(ctx.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        playerMovement.SetIsSprinting(ctx.ReadValueAsButton());
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
            return;

        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            firstPersonCamera.SetInputValues(ctx.ReadValue<Vector2>() * Time.deltaTime);
            lookInputs = Vector2.zero;
        }
        else
        {
            lookInputs = ctx.ReadValue<Vector2>() * 35f;
        }
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            PauseMenu.I.TogglePause();
    }

    private void Update()
    {
        if (objectHolder.HasItem)
            if (usingObject)
                objectHolder.ContinueUsing();
    }

    private void FixedUpdate()
    {
        if (playerInput.currentControlScheme != "Keyboard&Mouse")
            firstPersonCamera.SetInputValues(lookInputs * Time.fixedDeltaTime);
    }

    public void SetState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.None: playerInput.enabled = true; break;
            case PlayerState.Laptop: playerInput.enabled = false; break;
        }
    }

    public CameraAnimator CameraAnimator => cameraAnimator;
}

public enum PlayerState
{
    None,
    Laptop
}