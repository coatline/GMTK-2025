using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopChoiceUI : MonoBehaviour
{
    [SerializeField] TMP_Text costText;
    [SerializeField] Button buyButton;
    [SerializeField] Image itemPreview;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text boughtBefore;

    ItemType item;

    public void Setup(ItemType item)
    {
        this.item = item;
        UpdateUI();
    }

    public void UpdateUI()
    {
        itemPreview.sprite = item.sprite;
        costText.text = $"${item.price:F00}";
        buyButton.interactable = GameManager.I.Money >= item.price;
        boughtBefore.text = $"bought {GameManager.I.GetBoughtCount(item)}";
    }

    public void Buy()
    {
        GameManager.I.BuyItem(item);
    }
}
