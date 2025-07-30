using UnityEngine;

public class ChoreData
{
    public event System.Action Completed;
    public event System.Action<float> ProgressChanged;

    public readonly ChoreType type;

    public int sequentialMissedDays;
    public float percentageComplete;
    public int totalMissedDays;
    public bool complete;

    public ChoreData(ChoreType type)
    {
        this.type = type;
    }

    public void Complete()
    {
        complete = true;
        Completed?.Invoke();
    }

    public void RecordDay()
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
