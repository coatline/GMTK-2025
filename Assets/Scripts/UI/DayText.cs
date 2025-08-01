using TMPro;
using UnityEngine;

public class DayText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] BounceAnimation bounceAnimation;

    void Start()
    {
        TimeManager.I.NewDay += NewDay;
    }

    private void NewDay()
    {
        text.text = $"Day {TimeManager.I.Day}";
        bounceAnimation.Bounce(10f, 1);
    }
}
