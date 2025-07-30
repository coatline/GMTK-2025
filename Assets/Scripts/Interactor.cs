using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject interactIndicator;
    [SerializeField] PlayerController playerController;

    IInteractable currentlyHighlighted;

    void FixedUpdate()
    {
        if (CantInteract)
            return;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 4f))
        {
            currentlyHighlighted = hit.collider.gameObject.GetComponentInParent<IInteractable>();

            DebugMenu.I.DisplayValue("inView", hit.collider.gameObject);
        }
        else
            currentlyHighlighted = null;

        interactIndicator.SetActive(CanInteract());
    }

    public void TryInteract()
    {
        if (CanInteract())
        {
            currentlyHighlighted.Interact(this);
            currentlyHighlighted = null;
        }
    }

    bool CanInteract() => CantInteract == false && currentlyHighlighted != null && currentlyHighlighted.CanInteract(this);

    public PlayerController PlayerController => playerController;
    public bool CantInteract { get; set; }
}
