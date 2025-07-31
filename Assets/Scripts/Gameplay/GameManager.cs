using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event System.Action<float> MoneyChanged;

    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] TMP_Text bedtimeText;
    [SerializeField] float bedtimeHour;
    [SerializeField] Bed bed;

    PlayerStateController player;

    float money;
    public float Money
    {
        get => money;
        set
        {
            money = value;
            MoneyChanged?.Invoke(money);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        player = Instantiate(playerStateController);
        bedtimeText.text = $"{TimeManager.GetTimeString(bedtimeHour)}";
    }

    private IEnumerator Start()
    {
        yield return null;
        player.SleepController.SetSleeping(bed);
    }
}
