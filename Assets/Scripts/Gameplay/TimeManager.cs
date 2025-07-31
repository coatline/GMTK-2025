using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public event System.Action NewDay;

    [Header("Time Settings")]
    [Range(0f, 24f)]
    [SerializeField] float currentHour;
    [SerializeField] float timeMultiplier;

    List<TimedCallback> timedCallbacks;

    private void Start()
    {
        timedCallbacks = new List<TimedCallback>();
        RegisterDailyFunction(new TimedCallback(24, NewDay));
    }

    void UpdateTime()
    {
        float prevHour = currentHour;
        currentHour += WorldDeltaTime;

        for (int i = 0; i < timedCallbacks.Count; i++)
        {
            TimedCallback cb = timedCallbacks[i];
            if (!cb.triggeredToday && prevHour < cb.targetHour && currentHour >= cb.targetHour)
            {
                cb.function?.Invoke();
                cb.triggeredToday = true;
            }
        }

        if (currentHour >= 25)
        {
            currentHour = 1;

            for (int i = 0; i < timedCallbacks.Count; i++)
                timedCallbacks[i].triggeredToday = false;
        }

    }

    void Update()
    {
        UpdateTime();
    }

    public float TimeNormalized => currentHour / 24f;
    public float WorldDeltaTime => Time.deltaTime * (timeMultiplier / 60f);
    public string GetCurrentTimeString() => GetTimeString(currentHour);
    public void SetTimeMultiplier(float multiplier) => timeMultiplier = multiplier;
    public void RegisterDailyFunction(TimedCallback timedCallback) => timedCallbacks.Add(timedCallback);
    public void RemoveTimedCallback(TimedCallback timedCallback) => timedCallbacks.Remove(timedCallback);

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

        string ampm = hour < 12f ? "am" : "pm";
        return $"{displayHour}:{displayMinutes:00}{ampm}";
    }
}

public class TimedCallback
{
    public float targetHour;
    public bool triggeredToday;
    public System.Action function;

    public TimedCallback(float targetHour, Action function, bool triggeredToday = false)
    {
        this.triggeredToday = triggeredToday;
        this.targetHour = targetHour;
        this.function = function;
    }
}