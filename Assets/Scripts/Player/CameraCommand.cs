using UnityEngine;
using System;

[System.Serializable]
public class CameraCommand
{
    public event Action Finished;

    public Transform targetTransform;
    public Vector3 lookDirection;
    public float animationDuration;

    public CameraCommand(Transform target, Vector3 lookDirection, float animationDuration)
    {
        this.targetTransform = target;
        this.lookDirection = lookDirection.normalized;
        this.animationDuration = Mathf.Max(0.01f, animationDuration);
    }

    public void Finish()
    {
        Finished?.Invoke();
    }
}
