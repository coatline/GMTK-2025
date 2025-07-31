using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] MeshRenderer soilMeshRenderer;
    [SerializeField] MeshRenderer plantMeshRenderer;

    [SerializeField] float hoursToDry;
    [SerializeField] float hoursToDie;

    [SerializeField] Color wateredSoilColor;
    [SerializeField] Color drySoilColor;

    [SerializeField] Color healthyPlantColor;
    [SerializeField] Color deadPlantColor;

    MaterialPropertyBlock soilPropertyBlock;
    MaterialPropertyBlock plantPropertyBlock;

    float waterPercentage;
    float plantHealthPercentage;

    private void Awake()
    {
        soilPropertyBlock = new MaterialPropertyBlock();
        plantPropertyBlock = new MaterialPropertyBlock();

        soilMeshRenderer.GetPropertyBlock(soilPropertyBlock);
        plantMeshRenderer.GetPropertyBlock(plantPropertyBlock);

        PlantHealthPercentage = 1f;
        WaterPercentage = 0.75f;
    }

    public void Water(float amount)
    {
        WaterPercentage += amount;
    }

    void Update()
    {
        WaterPercentage -= TimeManager.I.WorldDeltaTime / hoursToDry;

        float deathRate = 0f;
        if (WaterPercentage <= 0.05f)
            deathRate = 1f;
        else if (WaterPercentage < 0.2f)
            deathRate = 0.5f;

        PlantHealthPercentage -= TimeManager.I.WorldDeltaTime * hoursToDie * deathRate;
    }

    float WaterPercentage
    {
        get => waterPercentage;
        set
        {
            waterPercentage = Mathf.Clamp01(value);
            soilPropertyBlock.SetColor("_BaseColor", Color.Lerp(drySoilColor, wateredSoilColor, value));
            soilMeshRenderer.SetPropertyBlock(soilPropertyBlock);
        }
    }

    float PlantHealthPercentage
    {
        get => plantHealthPercentage;
        set
        {
            if (plantMeshRenderer == null) return;

            plantHealthPercentage = Mathf.Clamp01(value);
            plantPropertyBlock.SetColor("_BaseColor", Color.Lerp(deadPlantColor, healthyPlantColor, value));
            plantMeshRenderer.SetPropertyBlock(plantPropertyBlock);

            if (plantHealthPercentage == 0f)
                Destroy(plantMeshRenderer.gameObject);
        }
    }
}
