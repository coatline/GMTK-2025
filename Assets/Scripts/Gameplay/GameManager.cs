using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event System.Action<float> MoneyChanged;

    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] DeliveryBox deliveryBoxPrefab;
    [SerializeField] Transform deliverySpot;
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

    public void BuyItem(ItemType item)
    {
        Money -= item.price;
        ItemType itemToDeliver = item;
        TimeManager.I.ScheduleFunction(new TimedCallback(10, () => { DeliverItem(itemToDeliver); }, true, true));
    }

    void DeliverItem(ItemType item)
    {
        print($"Delivered {item.name}!");
        DeliveryBox deliveryBox = Instantiate(deliveryBoxPrefab, deliverySpot.position, Quaternion.LookRotation(deliverySpot.forward));
        deliveryBox.Setup(item);
    }
}
