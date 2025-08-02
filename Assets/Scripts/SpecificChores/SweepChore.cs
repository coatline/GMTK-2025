using System.Collections.Generic;
using UnityEngine;

public class SweepChore : ChoreStation
{
    [SerializeField] Leaf leafPrefab;
    [SerializeField] Vector2 leafSpawnArea;
    [SerializeField] Transform leafSpawnCenter;

    [SerializeField] int minLeavesPerDay;
    [SerializeField] int maxLeavesPerDay;

    int totalLeaves;
    int remainingLeaves;

    protected override void NewDay()
    {
        print("New Day");

        // Place leaves
        int newTotal = GetTotalLeaves();
        totalLeaves += newTotal;
        remainingLeaves = totalLeaves;

        for (int i = 0; i < newTotal; i++)
        {
            Leaf newLeaf = Instantiate(leafPrefab, leafSpawnCenter.position + new Vector3(Random.Range(-leafSpawnArea.x, leafSpawnArea.x), 0, Random.Range(-leafSpawnArea.y, leafSpawnArea.y)), Quaternion.Euler(0, 0, Random.Range(0, 360f)), transform);
            newLeaf.LeftPorch += Leaf_Destroyed;
        }
    }

    private void Leaf_Destroyed(Leaf leaf)
    {
        leaf.LeftPorch -= Leaf_Destroyed;
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
        totalLeaves = 0;
        base.Complete();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (leafSpawnCenter == null) return;

        Gizmos.color = Color.green;
        Vector3 center = leafSpawnCenter.position + new Vector3(0, 0.05f, 0);
        Vector3 size = new Vector3(leafSpawnArea.x * 2f, 0.1f, (leafSpawnArea.y * 2f));

        Gizmos.DrawWireCube(center, size);
    }
#endif
}
