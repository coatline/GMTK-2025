using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Awake()
    {
        PauseMenu.Paused += ReleaseCursor;
        PauseMenu.Resumed += LockCursor;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        PauseMenu.Paused -= ReleaseCursor;
        PauseMenu.Resumed -= LockCursor;
    }
}
