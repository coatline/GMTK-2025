using UnityEngine;

public class Broom : HoldableObject
{
    [SerializeField] float sweepForce;
    [SerializeField] Transform sweepColliderPosition;

    public override void StartUsing(Vector2 direction)
    {
        Collider[] hits = Physics.OverlapBox(sweepColliderPosition.position, new Vector3(1, 0.5f, 1), Quaternion.identity);

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null && rb.name != "Player")
                rb.AddForce(transform.forward * sweepForce);
        }

        base.StartUsing(direction);
    }

    protected override void LeaveHand()
    {
        base.LeaveHand();
    }
}
