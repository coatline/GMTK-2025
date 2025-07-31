using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float cameraPitchLimit = 60f;
    [SerializeField] float lookSensitivity = 1.5f;
    [SerializeField] float smoothing = 2f;
    [SerializeField] Transform playerMesh;
    [SerializeField] Camera cam;

    Vector2 currentLookingPosition;
    Vector2 smoothedVelocity;
    Coroutine lerpCoroutine;

    public void SetCurrentLookingPosition(Vector2 lookingPosition)
    {
        currentLookingPosition = lookingPosition;
    }

    public void MoveCamera(Vector2 inputValues)
    {
        inputValues *= smoothing * lookSensitivity;

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1 / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1 / smoothing);

        currentLookingPosition += smoothedVelocity;

        currentLookingPosition.y = Mathf.Clamp(currentLookingPosition.y, -cameraPitchLimit, cameraPitchLimit);

        ApplyRotation(currentLookingPosition);
    }

    public void LerpToLookDirection(Vector3 worldDirection, float duration)
    {
        if (lerpCoroutine != null) StopCoroutine(lerpCoroutine);
        lerpCoroutine = StartCoroutine(LerpLookRoutine(worldDirection, duration));
    }

    IEnumerator LerpLookRoutine(Vector3 worldDirection, float duration)
    {
        Vector3 flatForward = new Vector3(worldDirection.x, 0, worldDirection.z).normalized;
        float targetYaw = Quaternion.LookRotation(flatForward).eulerAngles.y;
        float targetPitch = -Mathf.Asin(worldDirection.normalized.y) * Mathf.Rad2Deg;

        // Clamp pitch
        targetPitch = Mathf.Clamp(targetPitch, -cameraPitchLimit, cameraPitchLimit);

        Vector2 start = currentLookingPosition;
        Vector2 end = new Vector2(targetYaw, targetPitch);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            currentLookingPosition = Vector2.Lerp(start, end, t);
            ApplyRotation(currentLookingPosition);
            yield return null;
        }

        currentLookingPosition = end;
        ApplyRotation(currentLookingPosition);
        lerpCoroutine = null;
    }

    void ApplyRotation(Vector2 look)
    {
        cam.transform.localRotation = Quaternion.Euler(-look.y, 0f, 0f);
        playerMesh.localRotation = Quaternion.Euler(0f, look.x, 0f);
    }
}
