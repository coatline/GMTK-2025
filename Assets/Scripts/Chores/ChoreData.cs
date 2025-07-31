using UnityEngine;

public class ChoreData
{
    public event System.Action<ChoreData> Completed;
    public event System.Action<float> ProgressChanged;

    public readonly ChoreType Type;

    float percentageComplete;
    public float PercentageComplete
    {
        get => percentageComplete;
        set
        {
            percentageComplete = value;
            ProgressChanged?.Invoke(percentageComplete);
        }
    }

    public int sequentialMissedDays;
    public int totalMissedDays;
    public bool complete;

    public ChoreData(ChoreType type)
    {
        this.Type = type;
    }

    public void Complete()
    {
        complete = true;
        Completed?.Invoke(this);
    }

    public void DayFinished()
    {
        if (complete)
        {
            sequentialMissedDays = 0;
            complete = false;
        }
        else
        {
            totalMissedDays++;
            sequentialMissedDays++;
        }
    }
}
