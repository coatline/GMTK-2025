using System;
using UnityEngine;

public abstract class ChoreStation : MonoBehaviour
{
    [SerializeField] ChoreType choreType;

    protected ChoreData choreData;

    void Start()
    {
        choreData = ChoreManager.I.GetChoreDataFromType(choreType);
        TimeManager.I.NewDay += NewDay;
        NewDay();
    }

    protected abstract void NewDay();

    protected virtual void Complete()
    {
        choreData.Complete();
    }
}
