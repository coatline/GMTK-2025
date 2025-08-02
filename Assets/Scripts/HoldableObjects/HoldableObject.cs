using UnityEngine;

public class HoldableObject : MonoBehaviour, IInteractable
{
    public event System.Action PickedUp;
    public event System.Action LeftHand;
    public event System.Action Destroyed;

    [SerializeField] protected Rigidbody rb;
    [SerializeField] float dropForce;

    public bool BeingHeld { get; protected set; }
    public Rigidbody Rigidbody => rb;


    public void Drop(Vector3 direction)
    {
        LeaveHand();
        rb.linearVelocity = dropForce * direction;
    }

    protected virtual void LeaveHand()
    {
        rb.detectCollisions = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        BeingHeld = false;

        LeftHand?.Invoke();
    }

    public virtual void Pickup()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = Vector3.zero;

        rb.detectCollisions = false;
        rb.isKinematic = true;
        rb.useGravity = false;

        PickedUp?.Invoke();
    }

    public void Hold(Vector3 position, Vector3 rotation)
    {
        BeingHeld = true;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    public void Interact(Interactor interactor)
    {
        interactor.ObjectHolder.Pickup(this);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public virtual void StartUsing(Vector2 direction) { }
    public virtual void ContinueUsing(Vector3 direction) { }
    public virtual void FinishUsing(Vector3 direction) { }
    public virtual string InteractText => $"pickup {name}";
    public bool CanInteract(Interactor interactor) => interactor.ObjectHolder.HasItem == false;
    public virtual bool RotateVertically => false;
}