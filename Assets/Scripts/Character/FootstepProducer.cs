using UnityEngine;

public class FootstepProducer : MonoBehaviour
{
    [SerializeField] float footstepDistance;
    [SerializeField] SoundType footstepSound;
    [SerializeField] GroundDetector groundDetector;

    float distanceTraveled;
    Vector3 prevPosition;
    bool groundedOnce;

    private void Start()
    {
        if (groundDetector != null)
            groundDetector.JustGrounded += GroundDetector_JustGrounded;

        prevPosition = transform.position;
    }

    private void GroundDetector_JustGrounded()
    {
        // Don't make a sound on the first frame.
        if (groundedOnce == false)
        {
            groundedOnce = true;
            return;
        }

        // Make a footstep sound on land on the ground
        distanceTraveled = footstepDistance;
    }

    void Update()
    {
        if (groundDetector == null || groundDetector.IsOnGround)
        {
            float distanceInLastFrame = Vector3.Distance(new Vector3(prevPosition.x, 0, prevPosition.z), new Vector3(transform.position.x, 0, transform.position.z));
            //distanceTraveled += Mathf.Clamp(distanceInLastFrame, 0, footstepDistance);

            // If we went further than a footstep in a frame, we probably teleported.
            if (distanceInLastFrame < footstepDistance)
                distanceTraveled += distanceInLastFrame;

            if (distanceTraveled >= footstepDistance)
            {
                distanceTraveled = 0;
                SoundPlayer.I.PlaySound(footstepSound, transform.position);
            }
        }

        prevPosition = transform.position;
    }
}
