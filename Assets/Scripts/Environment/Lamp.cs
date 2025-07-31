using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    [SerializeField] MeshRenderer bulbMeshRenderer;
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;
    [SerializeField] Light pointLight;

    bool on;

    void Awake()
    {
        if (pointLight.gameObject.activeSelf)
            Toggle();
        else
        {
            on = true;
            Toggle();
        }
    }

    void Toggle()
    {
        on = !on;

        if (on)
            bulbMeshRenderer.material = onMaterial;
        else
            bulbMeshRenderer.material = offMaterial;

        pointLight.gameObject.SetActive(on);
    }

    public void Interact(Interactor interactor) => Toggle();
    public bool CanInteract(Interactor interactor) => true;
    public string InteractText => on ? "turn off" : "turn on";
}
