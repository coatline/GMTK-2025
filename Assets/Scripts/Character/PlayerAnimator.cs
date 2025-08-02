using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    void Update()
    {
        float fowardValue = Vector3.Dot(rb.linearVelocity, transform.forward);
        animator.SetFloat("Forward", fowardValue);
    }
}
