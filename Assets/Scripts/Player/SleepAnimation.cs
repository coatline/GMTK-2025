using UnityEngine;
using UnityEngine.UI;

public class SleepAnimation : MonoBehaviour
{
    [SerializeField] Image visionBlockImage;

    public void GoToSleep()
    {
        visionBlockImage.CrossFadeAlpha(1f, 1f, true);
    }

    public void GetUp()
    {
        visionBlockImage.CrossFadeAlpha(0, 1f, true);
    }
}
