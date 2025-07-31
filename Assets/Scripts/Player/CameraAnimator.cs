using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    [SerializeField] Transform cameraNormalPosition;
    [SerializeField] float followSpeed = 5f;
    [SerializeField] Camera cam;

    CameraCommand currentCommand;
    float percentageComplete;

    Vector3 startPosition;
    Quaternion startRotation;


    public void Animate(CameraCommand target)
    {
        currentCommand = target;
        percentageComplete = 0f;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (currentCommand == null) return;

        percentageComplete = Mathf.Clamp01(percentageComplete + (Time.deltaTime / currentCommand.animationDuration));

        transform.position = Vector3.Lerp(startPosition, currentCommand.targetTransform.position, percentageComplete);

        if (currentCommand.lookDirection != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(currentCommand.lookDirection);
            transform.rotation = Quaternion.Slerp(startRotation, targetRot, percentageComplete);
        }

        // add rotating to face direction of currentCommand.lookDirection

        if (percentageComplete >= 1f)
        {
            transform.position = currentCommand.targetTransform.position;

            if (currentCommand.lookDirection != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(currentCommand.lookDirection);
                transform.rotation = targetRot;
            }

            currentCommand.Finish();
            currentCommand = null;
        }
    }

    public Transform CameraNormalPosition => cameraNormalPosition;
}
