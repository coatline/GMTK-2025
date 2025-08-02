using UnityEngine;
using UnityEngine.AI;

public class ParentSleepState : ParentState
{
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
        isLyingDown = true;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        Debug.Log("Parent is lying down in bed.");
    }

    public override void Exit()
    {
        print("Exiting Sleep!");
        transform.rotation = Quaternion.identity;
        sleep = 0;
    }

    public override float MinDistance => 1.5f;
    public override Transform Target => bed.transform;
}

//public enum StateType
//{
//    Idle,
//    Moving,
//    Working
//}