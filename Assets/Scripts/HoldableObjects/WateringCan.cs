using UnityEngine;

public class WateringCan : HoldableObject
{
    [SerializeField] float spawnWaterInterval;
    [SerializeField] WaterDrop waterDropPrefab;
    [SerializeField] Transform waterSpawnPosition;

    IntervalTimer flowTimer;

    private void Awake()
    {
        flowTimer = new IntervalTimer(spawnWaterInterval, true);
    }

    public override void ContinueUsing(Vector3 direction)
    {
        if (flowTimer.DecrementIfRunning(Time.deltaTime))
        {
            SpawnDroplet();
            flowTimer.Start();
        }
    }

    void SpawnDroplet()
    {
        WaterDrop drop = Instantiate(waterDropPrefab, waterSpawnPosition.position, Quaternion.identity);
    }
}
