using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] LaptopController laptopController;
    [SerializeField] PlayerController playerController;
    [SerializeField] SleepController sleepController;
    [SerializeField] CameraAnimator cameraAnimator;

    PlayerState currentState;

    private void Awake()
    {
        playerController.Activated += SetState;
        laptopController.Activated += SetState;
        sleepController.Activated += SetState;
    }

    void SetState(PlayerState state)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = state;
    }

    public CameraAnimator CameraAnimator => cameraAnimator;
    public SleepController SleepController => sleepController;
    public PlayerController PlayerController => playerController;
    public LaptopController LaptopController => laptopController;
}