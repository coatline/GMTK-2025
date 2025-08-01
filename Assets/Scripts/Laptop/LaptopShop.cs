using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaptopShop : MonoBehaviour
{
    [SerializeField] ItemType[] stock;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] Transform shopChoiceHolder;
    [SerializeField] ShopChoiceUI shopChoiceUIPrefab;

    List<ShopChoiceUI> choices;

    private void Start()
    {
        choices = new List<ShopChoiceUI>();

        moneyText.text = $"${GameManager.I.Money}";

        foreach (ItemType item in stock)
        {
            ShopChoiceUI shopChoiceUI = Instantiate(shopChoiceUIPrefab, shopChoiceHolder);
            shopChoiceUI.Setup(item);
            choices.Add(shopChoiceUI);
        }

        GameManager.I.MoneyChanged += MoneyChanged;
    }

    private void MoneyChanged(float money)
    {
        moneyText.text = $"${money}";

        foreach (ShopChoiceUI choice in choices)
            choice.UpdateUI();
    }
}
