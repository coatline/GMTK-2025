using UnityEngine;

public class CharacterNeeds : MonoBehaviour
{
    [Range(0f, 1f)] public float hunger = 0.5f;
    [Range(0f, 1f)] public float sleep = 1f;

    [SerializeField] float inGameHoursTilStarved = 36f;
    [SerializeField] float inGameHoursTilExhausted = 36f;

    public System.Action<float, float> OnNeedsChanged;

    void Update()
    {
        hunger = Mathf.Clamp(hunger - TimeManager.I.HoursDeltaTime / inGameHoursTilStarved, 0, 100);
        sleep = Mathf.Clamp(sleep - TimeManager.I.HoursDeltaTime / inGameHoursTilExhausted, 0, 100);
        OnNeedsChanged?.Invoke(hunger, sleep);
    }

    public void Eat(float amount) => hunger = Mathf.Clamp01(hunger + amount);
    public void Sleep(float amount) => sleep = Mathf.Clamp01(sleep + amount);
}
