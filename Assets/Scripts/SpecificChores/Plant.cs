using UnityEngine;

public class Plant : MonoBehaviour
{
    public event System.Action<float> Watered;

    [SerializeField] MeshRenderer soilMeshRenderer;
    [SerializeField] MeshRenderer plantMeshRenderer;
    [SerializeField] BounceAnimation bounceAnimation;

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
        WaterPercentage = 0.5f;
    }

    public void Water(float amount)
    {
        WaterPercentage += amount;
    }

    void Update()
    {
        WaterPercentage -= TimeManager.I.HoursDeltaTime / hoursToDry;

        float deathRate = 0f;
        if (WaterPercentage <= 0.05f)
            deathRate = 1f;
        else if (WaterPercentage < 0.2f)
            deathRate = 0.5f;

        PlantHealthPercentage -= (TimeManager.I.HoursDeltaTime * deathRate) / hoursToDie;
    }

    void OnCollisionEnter(Collision collision)
    {
        WaterDrop waterDrop = collision.gameObject.GetComponent<WaterDrop>();

        if (waterDrop != null)
        {
            Water(0.05f);
            bounceAnimation.Bounce(10f, 0.01f);
            Destroy(waterDrop.gameObject);
            Watered?.Invoke(waterPercentage);
        }
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
