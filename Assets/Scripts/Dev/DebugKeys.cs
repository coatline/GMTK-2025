using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    System.Action[] debugActions;

    private void Awake()
    {
        debugActions = new System.Action[2]
        {
            ReloadScene,
            ()=>NextScene(1)
        };
    }

    private void Update()
    {
        for (int i = 0; i <= 9; i++)
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                if (i <= debugActions.Length)
                    debugActions[i - 1].Invoke();
    }

    public void ReloadScene()
    {
        SceneFader.I.ReloadCurrentScene(0.1f);
    }

    public void NextScene(int direction)
    {
        SceneFader.I.LoadSceneInDirection(direction);
    }
}
