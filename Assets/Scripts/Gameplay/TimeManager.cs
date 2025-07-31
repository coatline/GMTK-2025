using TMPro;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    [SerializeField] float currentHour = 6f; // Start at 6 AM
    [SerializeField] float timeSpeed; // In-game minutes per real second (60 = 1 hour per minute)

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
        currentTimeText.text = $"{GetCurrentTimeString()}";
    }

    void UpdateBedtimeDisplay()
    {
        bedtimeText.text = $"Bedtime: {GetTimeString(bedtimeHour)}";
    }

    public float TimeNormalized => currentHour / 24f;
    public string GetCurrentTimeString() => GetTimeString(currentHour);
    public static string GetTimeString(float hour, bool roundToHalfHour = true)
    {
        int totalMinutes = Mathf.FloorToInt(hour * 60f);

        if (roundToHalfHour)
        {
            int remainder = totalMinutes % 60;
            totalMinutes -= remainder;
            if (remainder >= 45) totalMinutes += 60;
            else if (remainder >= 15) totalMinutes += 30;
            // else keep it at :00
        }

        int displayHour = (totalMinutes / 60) % 12;
        if (displayHour == 0) displayHour = 12;
        int displayMinutes = totalMinutes % 60;

        string ampm = hour < 12f ? "AM" : "PM";
        return $"{displayHour}:{displayMinutes:00}{ampm}";
    }
}
