using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField] MeshRenderer bulbMeshRenderer;
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;
    [SerializeField] Light pointLight;

    public bool On { get; private set; }

    void Awake()
    {
        if (pointLight.gameObject.activeSelf)
            Toggle();
        else
        {
            On = true;
            Toggle();
        }
    }

    public void Toggle()
    {
        On = !On;

        if (On)
            bulbMeshRenderer.material = onMaterial;
        else
            bulbMeshRenderer.material = offMaterial;

        pointLight.gameObject.SetActive(On);
    }
}
