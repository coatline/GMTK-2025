using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    [SerializeField] List<ScheduledState> routine;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectionRange;
    [SerializeField] Transform lookPosition;

    private ParentState currentState;
    private int nextIndex = 0;

    void Update()
    {
        DetectPlayer();
        TryChangeState();
    }

    void TryChangeState()
    {
        float currentHour = TimeManager.I.GetHour;

        if (nextIndex < routine.Count && currentHour >= routine[nextIndex].hour)
        {
            SwitchState(routine[nextIndex].state);
            nextIndex++;
        }

        // Wrap at end of day
        if (currentHour < routine[0].hour) nextIndex = 0;
    }

    void SwitchState(ParentState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        if (currentState != null) currentState.Enter();
    }

    void DetectPlayer()
    {
        Ray ray = new Ray(lookPosition.transform.position, lookPosition.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, playerLayer))
        {
            Debug.Log("Player detected: " + hit.collider.name);
            // GameObject player = hit.collider.gameObject;
        }

        Debug.DrawRay(lookPosition.position, lookPosition.transform.forward * detectionRange, Color.red);
    }


    [System.Serializable]
    public class ScheduledState
    {
        public float hour;
        public ParentState state;
    }
}
