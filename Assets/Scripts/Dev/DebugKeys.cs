using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    System.Action[] debugActions;

    private void Awake()
    {
        debugActions = new System.Action[5]
        {
            ReloadScene,
            ()=>NextScene(1),
            ToggleDebugMenu,
            ToggleTime,
           ()=>GameManager.I.Money += 100
        };
    }

    private void Update()
    {
        for (int i = 0; i <= 9; i++)
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                if (i <= debugActions.Length)
                    debugActions[i - 1].Invoke();
    }

    void ReloadScene()
    {
        SceneFader.I.ReloadCurrentScene(0.1f);
    }

    void NextScene(int direction)
    {
        SceneFader.I.LoadSceneInDirection(direction);
    }

    void ToggleDebugMenu()
    {
        DebugMenu.I.Toggle();
    }

    void ToggleTime()
    {
        if (TimeManager.I.TimeMultiplier == 1)
            TimeManager.I.SetTimeMultiplier(60);
        else
            TimeManager.I.SetTimeMultiplier(1);
    }
}
