using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float flySpeed;
    [SerializeField] Rigidbody rb;

    Vector2 currentDirection;

    float currentSpeed;
    bool flying;
    bool jump;

    void Start()
    {
        currentSpeed = walkSpeed;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        var fwd = ((transform.forward * currentDirection.y) + (transform.right * currentDirection.x)).normalized * currentSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector3(fwd.x, rb.linearVelocity.y, fwd.z);
        DebugMenu.I.DisplayValue("linearVelocity", rb.linearVelocity.ToString());

        if (flying)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (jump)
        {
            rb.AddForce(new Vector3(0, jumpHeight), ForceMode.Impulse);
            jump = false;
        }
    }

    public void TryJump()
    {
        if (CanJump())
            if (rb.linearVelocity.y <= 0)
                jump = true;
    }

    public void TryFly(float dir)
    {
        if (flying)
            transform.Translate(new Vector3(0, dir * Time.fixedDeltaTime * flySpeed, 0));
    }

    public void ToggleFlying()
    {
        flying = !flying;
    }

    public void ToggleRunning()
    {
        if (currentSpeed == walkSpeed)
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;
    }

    bool CanJump() => Physics.Raycast(transform.position, -transform.up, 1.01f);

    public void SetDirection(Vector2 direction) => currentDirection = direction;
}
