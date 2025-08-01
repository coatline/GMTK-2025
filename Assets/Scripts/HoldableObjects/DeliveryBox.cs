using UnityEngine;

public class DeliveryBox : HoldableObject
{
    [SerializeField] ParticleSystem openParticlesPrefab;

    ItemType itemType;

    public void Setup(ItemType type)
    {
        transform.localScale = type.boxSize;
        this.itemType = type;
    }

    public override void StartUsing(Vector2 direction)
    {
        ParticleSystem ps = Instantiate(openParticlesPrefab, transform.position, transform.rotation);
        Instantiate(itemType.prefab, transform.position, transform.rotation);
        Destroy(gameObject);

        Destroy(ps.gameObject, 1f);
    }

    public override string InteractText => $"pickup box";
}