using System;
using UnityEngine;

public abstract class ChoreStation : MonoBehaviour
{
    public event System.Action Completed;

    [SerializeField] ChoreType choreType;

    protected ChoreData choreData;

    void Start()
    {
        choreData = ChoreManager.I.GetChoreDataFromType(choreType);
        Setup();
    }

    protected abstract void Setup();

    protected virtual void Complete()
    {
        choreData.Complete();
        Completed?.Invoke();
    }
}
