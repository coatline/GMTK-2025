using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event System.Action NewDay;

    public void EndDay()
    {
        NewDay?.Invoke();
    }
}
