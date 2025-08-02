using System.Collections.Generic;
using UnityEngine;

public class WashLaundryChore : ChoreStation
{
    [SerializeField] GameObject clothingPrefab;
    [SerializeField] DirtyLaundrySpawnArea[] dirtyLaundrySpawnAreas;
    List<GameObject> dirtyClothes;

    private void Start()
    {
        dirtyClothes = new List<GameObject>();
    }

    protected override void NewDay()
    {
        for (int i = 0; i < dirtyLaundrySpawnAreas.Length; i++)
        {
            DirtyLaundrySpawnArea area = dirtyLaundrySpawnAreas[i];
            int spawnAmount = area.GetSpawnAmount();

            for (int j = 0; j < spawnAmount; j++)
                dirtyClothes.Add(area.SpawnClothing());
        }
    }

    protected override void Complete()
    {
        base.Complete();
    }
}
