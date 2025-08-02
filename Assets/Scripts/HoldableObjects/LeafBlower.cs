using System.Collections.Generic;
using UnityEngine;

public class LeafBlower : HoldableObject
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform blowPosition;
    [SerializeField] float maxBlowRange;
    [SerializeField] float maxBlowForce;

    List<Vector3> rayDirections;


    private void Start()
    {
        rayDirections = new List<Vector3>();

        float horizontalFOV = 25f;
        float verticalFOV = 25f;
        float increment = 1f;


        int horRayCount = Mathf.FloorToInt(horizontalFOV / increment);
        int verRayCount = Mathf.FloorToInt(verticalFOV / increment);

        float halfHorFOV = horizontalFOV / 2f;
        float halfVerFOV = verticalFOV / 2f;

        for (int y = -verRayCount; y <= verRayCount; y++)
        {
            float pitch = y * increment;
            if (Mathf.Abs(pitch) > halfVerFOV) continue;

            for (int x = -horRayCount; x <= horRayCount; x++)
            {
                float yaw = x * increment;
                if (Mathf.Abs(yaw) > halfHorFOV) continue;

                Vector3 dir = Quaternion.Euler(pitch, yaw, 0) * Vector3.forward;
                rayDirections.Add(dir);
            }
        }
    }

    public override void StartUsing(Vector2 direction)
    {
        audioSource.Play();
    }

    public override void ContinueUsing(Vector3 direction)
    {
        Vector3 origin = blowPosition.position;

        for (int i = 0; i < rayDirections.Count; i++)
        {
            Vector3 dir = blowPosition.rotation * rayDirections[i];

            //dir.x *= blowPosition.forward.x;
            //dir.y *= blowPosition.forward.y;
            //dir.z *= blowPosition.forward.z;

            //dir *= blowPosition.forward;

            if (Physics.Raycast(origin, dir, out RaycastHit hit, maxBlowRange))
            {
                Rigidbody rb = hit.collider.attachedRigidbody;

                if (rb == null)
                {
                    Debug.DrawRay(origin, dir * maxBlowRange, Color.red);
                    continue;
                }

                float dist = Vector3.Distance(blowPosition.position, hit.collider.transform.position);

                rb.AddForce((dir + new Vector3(0, 0.5f)) * maxBlowForce * (1 - (dist / maxBlowRange)));
                Debug.DrawRay(origin, dir * maxBlowRange, Color.green);
            }
            else
                Debug.DrawRay(origin, dir * maxBlowRange, Color.red);
        }
    }

    public override void FinishUsing(Vector3 direction)
    {
        audioSource.Stop();
    }

    protected override void LeaveHand()
    {
        base.LeaveHand();
        audioSource.Stop();
    }

    public override bool RotateVertically => true;
}
