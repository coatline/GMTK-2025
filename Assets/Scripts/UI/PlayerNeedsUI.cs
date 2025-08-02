using UnityEngine;
using UnityEngine.UI;

public class PlayerNeedsUI : MonoBehaviour
{
    [SerializeField] CharacterNeeds needs;
    [SerializeField] Image hungerFill;
    [SerializeField] Image sleepFill;

    void Start()
    {
        needs.OnNeedsChanged += UpdateUI;
        UpdateUI(needs.hunger, needs.sleep);
    }

    void UpdateUI(float hunger, float sleep)
    {
        hungerFill.fillAmount = hunger;
        sleepFill.fillAmount = sleep;
    }
}
