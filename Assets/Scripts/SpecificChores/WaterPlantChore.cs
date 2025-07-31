using UnityEngine;

public class WaterPlantChore : ChoreStation
{
    [SerializeField] Plant plant;

    bool watching;

    protected override void NewDay()
    {
        if (watching) return;

        plant.Watered += Plant_Watered;
        watching = true;
    }

    private void Plant_Watered(float percentage)
    {
        choreData.PercentageComplete = percentage;

        if (percentage >= 0.99f)
            Complete();
    }

    protected override void Complete()
    {
        plant.Watered -= Plant_Watered;
        watching = false;
        base.Complete();
    }
}
