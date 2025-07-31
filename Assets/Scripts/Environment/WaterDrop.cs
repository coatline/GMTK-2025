using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    MaterialPropertyBlock propertyBlock;
    IntervalTimer lifeTimer;
    Color initialColor;
    Color finalColor;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propertyBlock);
        initialColor = meshRenderer.sharedMaterial.GetColor("_BaseColor");
        finalColor = initialColor - new Color(0, 0, 0, 1);
        lifeTimer = new IntervalTimer(1f, true);
    }

    private void Update()
    {
        if (lifeTimer.Decrement(Time.deltaTime))
            Destroy(gameObject);

        propertyBlock.SetColor("_BaseColor", Color.Lerp(initialColor, finalColor, lifeTimer.PercentageComplete));
        meshRenderer.SetPropertyBlock(propertyBlock);
    }
}
