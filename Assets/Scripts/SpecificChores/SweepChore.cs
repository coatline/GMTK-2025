using System.Collections.Generic;
using UnityEngine;

public class SweepChore : ChoreStation
{
    [SerializeField] Leaf leafPrefab;
    [SerializeField] Transform leafSpawnCenter;
    [SerializeField] Vector2 leafSpawnArea;

    [SerializeField] int minLeavesPerDay;
    [SerializeField] int maxLeavesPerDay;

    int totalLeaves;
    int remainingLeaves;

    protected override void Setup()
    {
        // Place leaves
        totalLeaves = GetTotalLeaves();
        remainingLeaves = totalLeaves;

        for (int i = 0; i < totalLeaves; i++)
        {
            Leaf newLeaf = Instantiate(leafPrefab, leafSpawnCenter.position + new Vector3(Random.Range(-leafSpawnArea.x, leafSpawnArea.x), 0, Random.Range(-leafSpawnArea.y, leafSpawnArea.y)), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            newLeaf.Destroyed += Leaf_Destroyed;
        }
    }

    private void Leaf_Destroyed(Leaf leaf)
    {
        leaf.Destroyed -= Leaf_Destroyed;
        choreData.PercentageComplete = 1 - (--remainingLeaves / (float)totalLeaves);

        if (remainingLeaves == 0)
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
