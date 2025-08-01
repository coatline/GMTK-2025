using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PlayerState
{
    const float USE_DEADZONE = .175f;

    [Header("References")]
    [SerializeField] FirstPersonCamera firstPersonCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ObjectHolder objectHolder;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] Interactor interactor;
    [SerializeField] Jumper jumper;

    Vector2 lookInputs;
    bool usingObject;

    void OnUse(InputAction.CallbackContext ctx)
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

    void OnDrop(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            if (objectHolder.HasItem)
                objectHolder.TryDrop();
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            jumper.TryJump();
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            interactor.TryInteract();
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        playerMovement.SetMoveInput(ctx.ReadValue<Vector2>());
    }

    void OnSprint(InputAction.CallbackContext ctx)
    {
        playerMovement.SetIsSprinting(ctx.ReadValueAsButton());
    }

    void OnLook(InputAction.CallbackContext ctx)
    {
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
            return;

        //if (playerInput.currentControlScheme == "Keyboard&Mouse")
        //{
        firstPersonCamera.MoveCamera(ctx.ReadValue<Vector2>() * Time.deltaTime);
        lookInputs = Vector2.zero;
        //}
        //else
        //{
        //    lookInputs = ctx.ReadValue<Vector2>() * 35f;
        //}
    }

    void OnPause(InputAction.CallbackContext ctx)
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

    //private void FixedUpdate()
    //{
    //    if (playerInput.currentControlScheme != "Keyboard&Mouse")
    //        firstPersonCamera.MoveCamera(lookInputs * Time.fixedDeltaTime);
    //}

    void EnableInputs()
    {
        playerInputs.Used += OnUse;
        playerInputs.Dropped += OnDrop;
        playerInputs.Jumped += OnJump;
        playerInputs.Interacted += OnInteract;
        playerInputs.Moved += OnMove;
        playerInputs.Sprinted += OnSprint;
        playerInputs.Looked += OnLook;
        playerInputs.Paused += OnPause;
    }

    void DisableInputs()
    {
        playerInputs.Used -= OnUse;
        playerInputs.Dropped -= OnDrop;
        playerInputs.Jumped -= OnJump;
        playerInputs.Interacted -= OnInteract;
        playerInputs.Moved -= OnMove;
        playerInputs.Sprinted -= OnSprint;
        playerInputs.Looked -= OnLook;
        playerInputs.Paused -= OnPause;
    }

    public void Activate() { NotifyActivated(); EnableInputs(); }
    public override void Exit()
    {
        DisableInputs();
        playerMovement.SetMoveInput(Vector2.zero);
    }
}