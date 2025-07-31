using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] Bed bed;

    PlayerStateController player;

    protected override void Awake()
    {
        base.Awake();
        player = Instantiate(playerStateController);
    }

    private IEnumerator Start()
    {
        yield return null;
        player.SleepController.Activate(bed);
        player.SleepController.SetSleeping();
    }
}
