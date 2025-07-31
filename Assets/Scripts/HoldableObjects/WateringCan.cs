using UnityEngine;

public class WateringCan : HoldableObject
{
    [SerializeField] float sweepForce;
    [SerializeField] Transform sweepColliderPosition;

    public override void StartUsing(Vector2 direction)
    {
        Collider[] hits = Physics.OverlapBox(sweepColliderPosition.position, new Vector3(1, 0.5f, 1), Quaternion.identity);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody.name == "Watering plant")
            {

            }

        }

        base.StartUsing(direction);
    }
}
