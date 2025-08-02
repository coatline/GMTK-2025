using UnityEngine;
using UnityEngine.AI;

public class ParentSleepState : ParentState
{
    [SerializeField] Animator animator;
    [SerializeField] Bed bed;

    bool isLyingDown;
    float sleep;

    public override void Enter()
    {
        isLyingDown = false;
    }

    public override void Perform()
    {
        if (sleep > 10)
        {
            parentController.SwitchState(null);
        }
        else
            sleep += TimeManager.I.MinutesDeltaTime;

        if (isLyingDown == false)
            LieDown();
    }

    void LieDown()
    {
        animator.enabled = false;
        isLyingDown = true;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.position = bed.SleepPosition.position;
        print("Parent is lying down in bed.");
    }

    public override void Exit()
    {
        animator.enabled = true;
        transform.rotation = Quaternion.identity;
        transform.position = bed.WakePosition.position;
        sleep = 0;
        print("Exiting Sleep!");
    }

    public override float MinDistance => 1.5f;
    public override Transform Target => bed.WakePosition;
}

//public enum StateType
//{
//    Idle,
//    Moving,
//    Working
//}