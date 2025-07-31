using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoreManager : Singleton<ChoreManager>
{
    public event System.Action ChoresUpdated;
    public List<ChoreData> AssignedChores { get; private set; }

    [SerializeField] List<ChoreStation> choreStations;
    [SerializeField] List<ChoreType> week1Chores;
    [SerializeField] List<ChoreType> week2Chores;

    Dictionary<ChoreType, ChoreData> getChoreData;
    List<ChoreData> everyChore;

    protected override void Awake()
    {
        base.Awake();

        everyChore = new List<ChoreData>();
        AssignedChores = new List<ChoreData>();
        getChoreData = new Dictionary<ChoreType, ChoreData>();

        for (int i = 0; i < DataLibrary.I.Chores.Length; i++)
        {
            ChoreType chore = DataLibrary.I.Chores[i];
            ChoreData data = new ChoreData(chore);
            getChoreData.Add(chore, data);
            everyChore.Add(data);
        }

        SetChores(week1Chores);
    }

    private void Start()
    {
        GameManager.I.NewDay += GameManager_NewDay;
    }

    private void GameManager_NewDay()
    {
        foreach (ChoreData data in AssignedChores)
            data.RecordDay();

        // Update chores list
    }

    void SetChores(List<ChoreType> newChores)
    {
        AssignedChores.Clear();

        for (int i = 0; i < newChores.Count; i++)
            AssignedChores.Add(getChoreData[newChores[i]]);
    }

    public ChoreData GetRandomAssignedChore() => AssignedChores[Random.Range(0, AssignedChores.Count)];
    public ChoreData GetRandomUnassignedChore() => UnassignedChores[Random.Range(0, UnassignedChores.Count)];
    public List<ChoreData> UnassignedChores => everyChore.Except(AssignedChores).ToList();
    public ChoreData GetChoreDataFromType(ChoreType type) => getChoreData[type];
}
