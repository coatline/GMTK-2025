using System.Collections.Generic;
using UnityEngine;

public class ParentView : MonoBehaviour
{
    public event System.Action SeePlayer;
    public event System.Action BrokeVisionOfPlayer;
    public event System.Action LostPlayer;

    Transform player;
    public Transform Player
    {
        get => player;
        set
        {
            if (value == player) return;

            // If we are about to set the player to null, record where we last saw him.
            if (value == null)
            {
                BrokeVisionOfPlayer?.Invoke();
                playerLastSeenPosition = player.transform.position;
                validLastSeen = true;
                player = value;
            }
            else
            {
                player = value;
                SeePlayer?.Invoke();
            }
        }
    }

    [SerializeField] Transform lookPosition;
    [SerializeField] float detectionRange;

    IntervalTimer pursuePlayerTimer;
    Vector2 playerLastSeenPosition;
    float timeWithoutSeeingPlayer;
    bool validLastSeen;


    private void Start()
    {
        // TODO: cache ray direction like in leaf blower
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Vector3 origin = lookPosition.position;

        float totalFOV = 60f;
        float increment = 5f;

        List<Vector3> directions = new List<Vector3>();

        int rayCount = Mathf.FloorToInt(totalFOV / increment);
        float halfFOV = totalFOV / 2f;

        for (int i = -rayCount; i <= rayCount; i++)
        {
            float angle = i * increment;
            if (Mathf.Abs(angle) > halfFOV) continue;

            Vector3 dir = Quaternion.Euler(0, angle, 0) * lookPosition.forward;
            directions.Add(dir);
        }

        bool sawPlayer = false;

        foreach (var dir in directions)
        {
            if (Physics.Raycast(origin, dir, out RaycastHit hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player") == false)
                {
                    Debug.DrawRay(origin, dir * detectionRange, Color.red);
                    return;
                }

                sawPlayer = true;
                Player = hit.collider.transform;
                pursuePlayerTimer.Start();
                Debug.DrawRay(origin, dir * detectionRange, Color.green);
            }
            else
                Debug.DrawRay(origin, dir * detectionRange, Color.red);
        }

        if (sawPlayer == false)
        {
            Player = null;

            if (pursuePlayerTimer.DecrementIfRunning(TimeManager.I.MinutesDeltaTime))
            {
                // Exit chase state (if in it). TODO
                validLastSeen = false;
                pursuePlayerTimer.Stop();
                LostPlayer?.Invoke();
            }
        }
    }
}
