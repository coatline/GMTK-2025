using UnityEngine;

public class WaterPlantChore : ChoreStation
{
    [SerializeField] Plant plant;

    private void Plant_Watered(float percentage)
    {
        if (percentage >= 0.99f)
            Complete();

        plant.Watered -= Plant_Watered;
    }

    protected override void NewDay()
    {
        plant.Watered += Plant_Watered;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
