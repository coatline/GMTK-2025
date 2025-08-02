using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] BounceAnimation bounceAnimation;

    float prevMoney;

    void Start()
    {
        GameManager.I.MoneyChanged += MoneyChanged;
        MoneyChanged(GameManager.I.Money);
    }

    private void MoneyChanged(float money)
    {
        if (prevMoney == money) return;

        text.text = $"${money:0.00}";
        bounceAnimation.Bounce(10f, Mathf.Clamp(Mathf.Abs((prevMoney - money) / 10), 0, 3));

        prevMoney = money;
    }
}
