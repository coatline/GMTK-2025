using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    // Player
    public event Action<InputAction.CallbackContext> Used;
    public event Action<InputAction.CallbackContext> Dropped;
    public event Action<InputAction.CallbackContext> Jumped;
    public event Action<InputAction.CallbackContext> Interacted;
    public event Action<InputAction.CallbackContext> Moved;
    public event Action<InputAction.CallbackContext> Sprinted;
    public event Action<InputAction.CallbackContext> Looked;
    public event Action<InputAction.CallbackContext> Paused;

    // Laptop
    public event Action<InputAction.CallbackContext> MovedCursor;
    public event Action<InputAction.CallbackContext> Backed;
    public event Action<InputAction.CallbackContext> Clicked;


    public Controls Controls { get; private set; }

    private void Awake()
    {
        Controls = new Controls();
        SubscribeToControls();
        Controls.Enable();
    }

    void SubscribeToControls()
    {
        StartCoroutine(DelaySubscription());
    }

    IEnumerator DelaySubscription()
    {
        yield return null;
        yield return null;

        // Player
        Controls.Player.Move.started += OnMove;
        Controls.Player.Move.performed += OnMove;
        Controls.Player.Move.canceled += OnMove;

        Controls.Player.Look.started += OnLook;
        Controls.Player.Look.performed += OnLook;
        Controls.Player.Look.canceled += OnLook;

        Controls.Player.Jump.started += OnJump;

        Controls.Player.Use.performed += OnUse;
        Controls.Player.Use.canceled += OnUse;

        Controls.Player.Drop.started += OnDrop;

        Controls.Player.Sprint.started += OnSprinted;
        Controls.Player.Sprint.canceled += OnSprinted;

        Controls.Player.Pause.started += OnPaused;

        Controls.Player.Interact.started += OnInteracted;

        // Laptop
        Controls.Laptop.MoveCursor.performed += OnMovedCursor;

        Controls.Laptop.Back.started += OnBack;

        Controls.Laptop.Click.started += OnClicked;
    }

    void UnsubscribeFromControls()
    {
        // Player
        Controls.Player.Move.started -= OnMove;
        Controls.Player.Move.performed -= OnMove;
        Controls.Player.Move.canceled -= OnMove;

        Controls.Player.Look.started -= OnLook;
        Controls.Player.Look.performed -= OnLook;
        Controls.Player.Look.canceled -= OnLook;

        Controls.Player.Jump.started -= OnJump;

        Controls.Player.Use.performed -= OnUse;
        Controls.Player.Use.canceled -= OnUse;

        Controls.Player.Drop.started -= OnDrop;

        Controls.Player.Sprint.started -= OnSprinted;
        Controls.Player.Sprint.canceled -= OnSprinted;

        Controls.Player.Pause.started -= OnPaused;

        Controls.Player.Interact.started -= OnInteracted;

        // Laptop
        Controls.Laptop.MoveCursor.performed -= OnMovedCursor;

        Controls.Laptop.Back.started -= OnBack;

        Controls.Laptop.Click.started -= OnClicked;
    }

    public Vector2 MovementVector { get; private set; }
    void OnMove(InputAction.CallbackContext context)
    {
        MovementVector = context.ReadValue<Vector2>();
        Moved?.Invoke(context);
    }
    void OnLook(InputAction.CallbackContext context) => Looked?.Invoke(context);
    void OnInteracted(InputAction.CallbackContext context) => Interacted?.Invoke(context);
    void OnPaused(InputAction.CallbackContext context) => Paused?.Invoke(context);
    void OnJump(InputAction.CallbackContext context) => Jumped?.Invoke(context);
    void OnUse(InputAction.CallbackContext context) => Used?.Invoke(context);
    void OnDrop(InputAction.CallbackContext context) => Dropped?.Invoke(context);
    void OnSprinted(InputAction.CallbackContext context) => Sprinted?.Invoke(context);

    void OnMovedCursor(InputAction.CallbackContext context) => MovedCursor?.Invoke(context);
    void OnBack(InputAction.CallbackContext context) => Backed?.Invoke(context);
    void OnClicked(InputAction.CallbackContext context) => Clicked?.Invoke(context);

    private void OnDestroy()
    {
        if (Controls != null)
            UnsubscribeFromControls();

        Looked = null;
        Interacted = null;
        Paused = null;
        Jumped = null;
        Moved = null;
        Used = null;
        Dropped = null;
        Sprinted = null;

        Controls.Disable();
        Controls.Dispose();
    }
}
