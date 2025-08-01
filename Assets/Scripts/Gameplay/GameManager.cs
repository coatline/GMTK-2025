using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event System.Action<float> MoneyChanged;

    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] DeliveryBox deliveryBoxPrefab;
    [SerializeField] Transform deliverySpot;

    [SerializeField] float bedtimeHour;
    [SerializeField] Bed bed;

    Dictionary<ItemType, int> itemToBought;

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
        itemToBought = new Dictionary<ItemType, int>();
        player = Instantiate(playerStateController);
    }

    private IEnumerator Start()
    {
        yield return null;
        player.SleepController.SetSleeping(bed);
    }

    public void BuyItem(ItemType item)
    {
        SoundPlayer.I.PlaySound("Purchase", transform.position);

        itemToBought[item] = itemToBought.GetValueOrDefault(item) + 1;
        Money -= item.price;

        ItemType itemToDeliver = item;
        TimeManager.I.ScheduleFunction(new TimedCallback(10, () => { DeliverItem(itemToDeliver); }, true, true));
    }

    void DeliverItem(ItemType item)
    {
        print($"Delivered {item.name}!");
        DeliveryBox deliveryBox = Instantiate(deliveryBoxPrefab, deliverySpot.position, Quaternion.LookRotation(deliverySpot.forward));
        deliveryBox.Setup(item);
        SoundPlayer.I.PlaySound("DeliverPackage", deliverySpot.position);
    }

    public int GetBoughtCount(ItemType item) => itemToBought.GetValueOrDefault(item);
}
