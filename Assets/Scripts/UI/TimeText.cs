using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Update()
    {
        text.text = $"{TimeManager.I.GetCurrentTimeString()}";
    }
}
