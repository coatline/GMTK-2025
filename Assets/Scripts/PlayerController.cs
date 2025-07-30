using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ObjectHolder playerHand;

    public void SetFocus(FocusControl focusControl)
    {
        Focus = focusControl;
    }

    public FocusControl Focus { get; private set; }
    public bool IsOccupied { get; set; }
    public ObjectHolder ObjectHolder => playerHand;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Interactor interactor;
    [SerializeField] FirstPersonCamera firstPersonCamera;
    [SerializeField] ObjectHolder objectHolder;
    [SerializeField] PlayerController playerController;

    //void Start()
    //{
    //    playerMovement.SetDirection(Vector2.zero);
    //}

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
