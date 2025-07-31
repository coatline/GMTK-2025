using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        GameManager.I.MoneyChanged += MoneyChanged;
    }

    private void MoneyChanged(float money)
    {
        text.text = $"${money}";
    }
}
