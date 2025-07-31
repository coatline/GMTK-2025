using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] CameraAnimator cameraAnimator;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerInput laptopInput;

    PlayerInput currentInput;

    public Laptop Laptop { get; private set; }

    IEnumerator Start()
    {
        playerInput.actions.FindActionMap("Laptop").Disable();
        playerInput.actions.FindActionMap("UI").Disable();
        playerInput.SwitchCurrentActionMap("Player");

        foreach (var map in playerInput.actions.actionMaps)
            Debug.Log(map.name + " enabled: " + map.enabled);

        yield return null;

        playerInput.actions.FindActionMap("Laptop").Disable();
        playerInput.actions.FindActionMap("UI").Disable();

        foreach (var map in playerInput.actions.actionMaps)
            Debug.Log(map.name + " enabled: " + map.enabled);

        yield return null;

        foreach (var map in playerInput.actions.actionMaps)
            Debug.Log(map.name + " enabled: " + map.enabled);

        yield return new WaitForSeconds(0.5f);
        foreach (var map in playerInput.actions.actionMaps)
            Debug.Log(map.name + " enabled: " + map.enabled);

    }

    public void SetLaptop(Laptop laptop)
    {
        Laptop = laptop;
        playerInput.SwitchCurrentActionMap("Laptop");
        //playerInput.enabled = false;
        //laptopInput.enabled = true;
    }

    public void SetNormal()
    {
        Laptop = null;
        playerInput.SwitchCurrentActionMap("Player");
        //playerInput.enabled = true;
        //laptopInput.enabled = false;
    }

    public CameraAnimator CameraAnimator => cameraAnimator;
}


public enum PlayerState
{
    None,
    Laptop
}
