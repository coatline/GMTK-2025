using TMPro;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Time Settings")]
    [Range(0f, 12f)]
    [SerializeField] float currentHour = 6f; // Start at 6 AM
    [SerializeField] float timeSpeed = 120f; // In-game minutes per real second (60 = 1 hour per minute)

    [Header("Bedtime")]
    [SerializeField] float bedtimeHour = 20f; // 8 PM

    [Header("UI")]
    [SerializeField] TMP_Text currentTimeText;
    [SerializeField] TMP_Text bedtimeText;

    void Start()
    {
        UpdateBedtimeDisplay();
    }

    void Update()
    {
        UpdateTime();
        UpdateTimeDisplay();
    }

    void UpdateTime()
    {
        currentHour += (Time.deltaTime * (timeSpeed / 60f));

        if (currentHour >= 25)
            currentHour = 1;
    }

    void UpdateTimeDisplay()
    {
        string ampm = currentHour < 12f ? "AM" : "PM";
        string formattedTime = $"{Mathf.FloorToInt(currentHour):00}:00 {ampm}";
        currentTimeText.text = $"Current Time: {formattedTime}";
    }

    void UpdateBedtimeDisplay()
    {
        string ampm = bedtimeHour < 12f ? "AM" : "PM";
        string formattedTime = $"{Mathf.FloorToInt(bedtimeHour):00}:00 {ampm}";
        bedtimeText.text = $"Bedtime: {formattedTime}";
    }
}
