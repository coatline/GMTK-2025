using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Leaf : HoldableObject
{
    public event System.Action<Leaf> LeftPorch;

    [SerializeField] float gravityScale = 0.3f;
    [SerializeField] float airSpinStrength = 30f;
    [SerializeField] float lateralThreshold = 0.5f;
    [SerializeField] float rotateTorqueStrength = 10f;

    bool IsGrounded => rb.linearVelocity.y <= 0f && rb.linearVelocity.y >= -0.1f;

    void Awake()
    {
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (BeingHeld) return;

        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);

        Vector3 lateralVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.angularVelocity = rb.linearVelocity;
        rb.linearVelocity += new Vector3(0, lateralVelocity.magnitude * Time.fixedDeltaTime, 0);

        if (!IsGrounded)
        {
            if (lateralVelocity.magnitude > lateralThreshold)
                RotatePerpendicularToMovement(lateralVelocity);
            else
                SpinLikeLeaf();
        }
    }

    void RotatePerpendicularToMovement(Vector3 lateralVelocity)
    {
        Vector3 targetForward = Vector3.Cross(lateralVelocity, Vector3.up);
        Quaternion target = Quaternion.LookRotation(targetForward, Vector3.up);

        Quaternion deltaRotation = target * Quaternion.Inverse(rb.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;

        Vector3 torque = axis.normalized * angle * Mathf.Deg2Rad * rotateTorqueStrength;
        rb.angularVelocity += torque;
    }

    void SpinLikeLeaf()
    {
        rb.AddTorque(airSpinStrength * Time.fixedDeltaTime * Random.onUnitSphere, ForceMode.VelocityChange);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PorchArea"))
        {
            LeftPorch?.Invoke(this);
            Destroy(gameObject, 5f);
        }
    }
}
