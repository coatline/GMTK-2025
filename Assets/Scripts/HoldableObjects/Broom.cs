using UnityEngine;

public class Broom : HoldableObject
{
    [SerializeField] float sweepForce;
    [SerializeField] Transform sweepColliderPosition;
    [SerializeField] ParticleSystem sweepParticleSystem;

    public override void StartUsing(Vector2 direction)
    {
        Collider[] hits = Physics.OverlapBox(sweepColliderPosition.position, new Vector3(1, 0.5f, 1), Quaternion.identity);

        SoundPlayer.I.PlaySound("SweepBroom", transform.position);

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null && rb.name != "Character")
            {
                SoundPlayer.I.PlaySound("LeafSweep", hit.transform.position);
                rb.AddForce((transform.forward + new Vector3(0, .25f, 0)) * sweepForce);
            }
        }

        sweepParticleSystem.Emit(10);
    }

    public override string InteractText => $"pickup broom";
}
