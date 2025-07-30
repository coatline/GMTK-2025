using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoreManager : Singleton<ChoreManager>
{
    public event System.Action ChoresUpdated;
    public List<ChoreData> AssignedChores { get; private set; }

    [SerializeField] List<ChoreStation> choreStations;

    Dictionary<ChoreType, ChoreData> choreToData;
    List<ChoreData> unassignedChores;
    List<ChoreData> everyChore;

    protected override void Awake()
    {
        base.Awake();

        everyChore = new List<ChoreData>();
        AssignedChores = new List<ChoreData>();
        choreToData = new Dictionary<ChoreType, ChoreData>();

        for (int i = 0; i < DataLibrary.I.Chores.Length; i++)
        {
            ChoreType chore = DataLibrary.I.Chores[i];
            ChoreData data = new ChoreData(chore);
            choreToData.Add(chore, data);
            everyChore.Add(data);
        }

        unassignedChores = new List<ChoreData>(everyChore);
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

    public void AssignChore(ChoreType type)
    {

    }

    public ChoreData GetChoreDataFromType(ChoreType type) => choreToData[type];

    IEnumerable<ChoreData> GetAssignedChores => AssignedChores;
}
