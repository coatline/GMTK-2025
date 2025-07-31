using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const float USE_DEADZONE = .175f;

    [Header("References")]
    [SerializeField] FirstPersonCamera firstPersonCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ObjectHolder objectHolder;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Interactor interactor;
    [SerializeField] Jumper jumper;

    public FocusState Focus { get; private set; }

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
        if (usingObject)
            objectHolder.ContinueUsing();
    }

    private void FixedUpdate()
    {
        if (playerInput.currentControlScheme != "Keyboard&Mouse")
            firstPersonCamera.SetInputValues(lookInputs * Time.fixedDeltaTime);
    }

    public void SetFocus(FocusState focusControl)
    {
        Focus = focusControl;
    }


    //void Update()
    //{
    //    if (playerController.Focus == null)
    //        PauseMenu.I.

    //    else if (Input.GetKeyDown(KeyCode.Escape))
    //        Cursor.lockState = CursorLockMode.None;

    //    if (playerController.IsOccupied) return;
    //    Interact();
    //    Debug();
    //    Item();

    //    Jump();
    //    LateralMovement();
    //    Mouse();
    //    Flying();
    //}

    //void Item()
    //{
    //    if (objectHolder.HasItem == false)
    //        return;

    //    if (Input.GetMouseButtonDown(0))
    //        objectHolder.StartUsing();
    //    else if (Input.GetMouseButton(0))
    //        objectHolder.ContinueUsing();
    //    else
    //        objectHolder.FinishUsing();

    //    if (Input.GetMouseButtonDown(1))
    //        objectHolder.Drop();
    //}

    //void Interact()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //        interactor.TryInteract();
    //}

    //void Mouse()
    //{
    //    Vector2 inputValues = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    //    firstPersonCamera.RotateCamera(inputValues);
    //}

    //void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        playerMovement.TryJump();
    //}

    //void LateralMovement()
    //{
    //    Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    //    playerMovement.SetDirection(inputs);

    //    if (Input.GetKeyDown(KeyCode.LeftControl))
    //        playerMovement.ToggleRunning();
    //}

    //void Flying()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //        playerMovement.TryFly(1);
    //    if (Input.GetKey(KeyCode.LeftShift))
    //        playerMovement.TryFly(-1);
    //}

    //void Debug()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    //    if (Input.GetKeyDown(KeyCode.F))
    //        playerMovement.ToggleFlying();

    //    if (Input.GetKeyDown(KeyCode.F1))
    //        DebugMenu.I.Toggle();
    //}
}
