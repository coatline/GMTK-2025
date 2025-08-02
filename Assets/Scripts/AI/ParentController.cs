using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentController : MonoBehaviour
{
    public ParentSpeechController Speaker => parentSpeechController;
    public ParentState CurrentState => currentState;
    public ParentView ParentView => parentView;
    public NavMeshAgent Nav => navMeshAgent;
    public Animator Animator => animator;

    [SerializeField] ParentSpeechController parentSpeechController;
    [SerializeField] List<ParentThought> thoughts;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] ParentView parentView;
    [SerializeField] float movementSpeed;
    [SerializeField] float angularSpeed;
    [SerializeField] float acceleration;
    [SerializeField] Animator animator;

    ParentThought currentThought;
    ParentState currentState;


    private void Start()
    {
        TimeManager.I.TimeMultiplierChanged += TimeMultiplierChanged;
        navMeshAgent.avoidancePriority = Random.Range(0, 100);
    }

    void Update()
    {
        DoBrain();
        Animate();
    }

    void DoBrain()
    {
        if (currentThought == null)
            SetThought(RandomThought());

        currentThought.Think();
        currentState?.Update();

        if (currentThought != null && currentState != null)
            DebugMenu.I.DisplayValue($"{name}", $"{currentThought.TypeName} : {currentState.ActionName}");
    }

    public void SetThought(ParentThought newThought)
    {
        if (currentThought == newThought) return;

        print($"{name} thinks: '{newThought}'");
        //SetState(null);

        if (currentThought != null) currentThought.Exit();
        currentThought = newThought;
        if (currentThought != null) currentThought.Enter();
    }

    public void SetState(ParentState newState)
    {
        if (currentState == newState) return;

        if (currentState != null) currentState.Exit();
        currentState = newState;
        if (currentState != null) currentState.Enter();
    }

    void Animate()
    {
        float fowardValue = Vector3.Dot(navMeshAgent.velocity, transform.forward);
        animator.SetFloat("Forward", fowardValue);
        //print($"{name} {animator.GetFloat("Foward")} {fowardValue}");
    }

    private void TimeMultiplierChanged(float multiplier)
    {
        navMeshAgent.speed = movementSpeed * TimeManager.I.TimeMultiplier;
        navMeshAgent.angularSpeed = angularSpeed * TimeManager.I.TimeMultiplier;
        navMeshAgent.acceleration = acceleration * TimeManager.I.TimeMultiplier;
    }

    ParentThought RandomThought() => thoughts[Random.Range(0, thoughts.Count)];
    public float GetDistanceFrom(Transform target) => Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));


    [System.Serializable]
    public class ScheduledThought
    {
        public float hour;
        public ParentThought thought;
    }
}