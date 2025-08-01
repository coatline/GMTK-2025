using System.Collections;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    [SerializeField] AnimationCurve bounceCurve;

    Vector3 originalScale;
    Coroutine routine;

    public void Bounce(float speed, float scale)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            transform.localScale = originalScale;
        }

        routine = StartCoroutine(Animate(speed, scale));
    }

    IEnumerator Animate(float speed, float scale)
    {
        originalScale = transform.localScale;

        float percent = 0;
        while (percent < 1)
        {
            transform.localScale = originalScale + (Vector3.one * bounceCurve.Evaluate(1 - percent) * scale);
            percent += Time.deltaTime * speed;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
