using UnityEngine;

public class Laundry : HoldableObject
{
    public event System.Action Cleaned;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Color[] colors;

    MaterialPropertyBlock materialPropertyBlock;

    Color cleanColor;
    Color dirtyColor;
    bool clean;

    private void Start()
    {
        cleanColor = colors[Random.Range(0, colors.Length)];

        materialPropertyBlock = new MaterialPropertyBlock();
        SetColor(dirtyColor);
    }

    public void SetClean()
    {
        clean = true;
        SetColor(cleanColor);
        Cleaned?.Invoke();
    }


    void SetColor(Color color)
    {
        materialPropertyBlock.SetColor("_BaseColor", color);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
