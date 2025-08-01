using UnityEngine;

[CreateAssetMenu()]
public class ItemType : ScriptableObject
{
    public float price;
    public Sprite sprite;
    public Vector3 boxSize;
    public HoldableObject prefab;
}
