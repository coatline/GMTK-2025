using System.Collections.Generic;
using UnityEngine;

public class SweepChore : ChoreStation
{
    [SerializeField] Leaf leafPrefab;
    [SerializeField] Transform leafSpawnCenter;
    [SerializeField] Vector2 leafSpawnArea;

    [SerializeField] int minLeavesPerDay;
    [SerializeField] int maxLeavesPerDay;

    List<Leaf> unsweptLeaves;

    protected override void Setup()
    {
        // Place leaves
        unsweptLeaves = new List<Leaf>();

        for (int i = 0; i < GetTotalLeaves(); i++)
        {
            Leaf newLeaf = Instantiate(leafPrefab, leafSpawnCenter.position + new Vector3(Random.Range(-leafSpawnArea.x, leafSpawnArea.x), 0, Random.Range(-leafSpawnArea.y, leafSpawnArea.y)), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            newLeaf.Destroyed += NewLeaf_Destroyed;
            unsweptLeaves.Add(newLeaf);
        }
    }

    private void NewLeaf_Destroyed(Leaf leaf)
    {
        unsweptLeaves.Remove(leaf);

        if (unsweptLeaves.Count == 0)
            Complete();
    }

    int GetTotalLeaves()
    {
        int totalLeaves = 0;
        for (int i = 0; i < choreData.sequentialMissedDays + 1; i++)
            totalLeaves += Random.Range(minLeavesPerDay, maxLeavesPerDay);
        return totalLeaves;
    }

    protected override void Complete()
    {
        print("complete");
        base.Complete();
    }
}
