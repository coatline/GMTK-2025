using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] Transform mirrorTransform;
    [SerializeField] Camera mirrorCamera;

    void UpdateMirrorCamera(Camera mainCamera)
    {
        //mirrorCamera.transform.rotation = Quaternion.LookRotation((mainCamera.transform.position - mirrorTransform.position));
    }

    void LateUpdate()
    {
        //UpdateMirrorCamera(Camera.main);
    }
}
