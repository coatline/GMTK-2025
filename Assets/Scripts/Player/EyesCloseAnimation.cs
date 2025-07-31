using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EyesCloseAnimation : MonoBehaviour
{
    [SerializeField] Image visionBlockImage;

    Coroutine fadeRoutine;

    public void StartFade(float targetAlpha, float fadeDuration)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeToAlpha(targetAlpha, fadeDuration));
    }

    IEnumerator FadeToAlpha(float targetAlpha, float fadeDuration)
    {
        Color color = visionBlockImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            visionBlockImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        visionBlockImage.color = color;
    }
}
