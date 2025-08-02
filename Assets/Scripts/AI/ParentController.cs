using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentController : MonoBehaviour
{
    public event System.Action SeePlayer;
    public event System.Action BrokeVisionOfPlayer;
    public event System.Action LostPlayer;

    //[SerializeField] List<ScheduledState> routine;
    [SerializeField] ParentSpeechController parentSpeechController;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] float pursuePlayerTime;
    [SerializeField] Transform lookPosition;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectionRange;
    [SerializeField] float movementSpeed;
    [SerializeField] float angularSpeed;
    [SerializeField] float acceleration;
    [SerializeField] Animator animator;
    [SerializeField] Bed bed;


    IntervalTimer pursuePlayerTimer;
    Vector2 playerLastSeenPosition;
    ParentState currentState;
    List<ParentState> states;
    bool validLastSeen;

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

    public NavMeshAgent Nav => navMeshAgent;
    public Animator Animator => animator;


    private void Start()
    {
        pursuePlayerTimer = new IntervalTimer(pursuePlayerTime);
        TimeManager.I.TimeMultiplierChanged += TimeMultiplierChanged;
        navMeshAgent.avoidancePriority = Random.Range(0, 100);
        states = new List<ParentState>()
        {
            new ParentWorkState(),
            new ParentSleepState(bed, this)
        };
    }

    void Update()
    {
        DetectPlayer();
        DoBrain();
        Animate();
    }

    void DoBrain()
    {
        if (currentState == null)
            ChooseRandomState();

        parentSpeechController.Say($"Doing {currentState.GetType().Name} {navMeshAgent.enabled}");
        currentState.Update();
    }

    void ChooseRandomState()
    {
        SetState(states[Random.Range(0, states.Count)]);
    }

    public void SetState(ParentState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        if (currentState != null) currentState.Enter();
    }

    public float GetDistanceFrom(Transform target) => Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));

    void DetectPlayer()
    {
        Vector3 origin = lookPosition.position;
        Vector3 forward = lookPosition.forward;

        float totalFOV = 60f;
        float increment = 5f;

        List<Vector3> directions = new List<Vector3>();

        int rayCount = Mathf.FloorToInt(totalFOV / increment);
        float halfFOV = totalFOV / 2f;

        for (int i = -rayCount; i <= rayCount; i++)
        {
            float angle = i * increment;
            if (Mathf.Abs(angle) > halfFOV) continue;

            Vector3 dir = Quaternion.Euler(0, angle, 0) * forward;
            directions.Add(dir);
        }

        bool sawPlayer = false;

        foreach (var dir in directions)
        {
            if (Physics.Raycast(origin, dir, out RaycastHit hit, detectionRange, playerLayer))
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

    
    void Animate()
    {
        float fowardValue = Vector3.Dot(navMeshAgent.velocity, transform.forward);
        animator.SetFloat("Forward", fowardValue);
        print($"{name} {animator.GetFloat("Foward")} {fowardValue}");
    }

    private void TimeMultiplierChanged(float multiplier)
    {
        navMeshAgent.speed = movementSpeed * TimeManager.I.TimeMultiplier;
        navMeshAgent.angularSpeed = angularSpeed * TimeManager.I.TimeMultiplier;
        navMeshAgent.acceleration = acceleration * TimeManager.I.TimeMultiplier;
    }

    [System.Serializable]
    public class ScheduledState
    {
        public float hour;
        public ParentState state;
    }
}