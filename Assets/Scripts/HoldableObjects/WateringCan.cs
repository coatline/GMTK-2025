using UnityEngine;

public class WateringCan : HoldableObject
{
    [SerializeField] float spawnWaterInterval;
    [SerializeField] WaterDrop waterDropPrefab;
    [SerializeField] Transform waterSpawnPosition;
    [SerializeField] AudioSource audioSource;

    IntervalTimer flowTimer;

    private void Awake()
    {
        flowTimer = new IntervalTimer(spawnWaterInterval, true);
    }

    public override void StartUsing(Vector2 direction)
    {
        audioSource.Play();
    }

    public override void ContinueUsing(Vector3 direction)
    {
        if (flowTimer.DecrementIfRunning(Time.deltaTime))
        {
            Instantiate(waterDropPrefab, waterSpawnPosition.position, Quaternion.identity);
            flowTimer.Start();
        }
    }

    public override void FinishUsing(Vector3 direction)
    {
        audioSource.Stop();
    }

    protected override void LeaveHand()
    {
        audioSource.Stop();
        base.LeaveHand();
    }

    public override string InteractText => $"pickup watering can";
}
