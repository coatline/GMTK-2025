using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] LaptopController laptopController;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] PlayerInput playerInput;

    IEnumerator Start()
    {
        playerInput.enabled = false;

        yield return null;

        playerInput.enabled = true;

        foreach (var map in playerInput.actions.actionMaps)
            map.Disable();

        playerInput.SwitchCurrentActionMap("Player");
    }

    public CameraAnimator CameraAnimator => cameraAnimator;
    public PlayerController PlayerController => playerController;
    public LaptopController LaptopController => laptopController;
}
