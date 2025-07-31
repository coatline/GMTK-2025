using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] LaptopController laptopController;
    [SerializeField] PlayerController playerController;
    [SerializeField] SleepController sleepController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] PlayerInput playerInput;

    private void Awake()
    {
        foreach (var map in playerInput.actions.actionMaps)
            map.Disable();
    }

    IEnumerator Start()
    {
        playerInput.enabled = false;

        yield return null;

        foreach (var map in playerInput.actions.actionMaps)
            map.Disable();

        playerInput.enabled = true;
        playerInput.SwitchCurrentActionMap("Player");
    }

    public CameraAnimator CameraAnimator => cameraAnimator;
    public SleepController SleepController => sleepController;
    public PlayerController PlayerController => playerController;
    public LaptopController LaptopController => laptopController;
}
