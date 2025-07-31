using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Light sunLight;

    [Header("Sun Settings")]
    [SerializeField] Gradient lightColorOverDay;
    [SerializeField] AnimationCurve intensityOverDay;
    [SerializeField] Vector3 sunRotationAtMidnight;

    [Header("Lighting")]
    [SerializeField] float maxSunIntensity = 1.2f;
    [SerializeField] float minSunIntensity = 0f;

    void Update()
    {
        UpdateSunRotation(TimeManager.I.TimeNormalized);
        UpdateSunLight(TimeManager.I.TimeNormalized);
    }

    void UpdateSunRotation(float timePercent)
    {
        float sunAngle = timePercent * 360;
        transform.localRotation = Quaternion.Euler(sunRotationAtMidnight + new Vector3(sunAngle, 0f, 0f));
    }

    void UpdateSunLight(float timePercent)
    {
        sunLight.intensity = Mathf.Lerp(minSunIntensity, maxSunIntensity, intensityOverDay.Evaluate(timePercent));
        sunLight.color = lightColorOverDay.Evaluate(timePercent);
    }
}
