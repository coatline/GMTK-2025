using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;

    ChoreData chore;

    public void Setup(ChoreData chore)
    {
        this.chore = chore;
        nameText.text = chore.Type.name;
        descriptionText.text = chore.Type.description;
        chore.Completed += Chore_Completed;
        chore.ProgressChanged += Chore_ProgressChanged;
    }

    private void Chore_ProgressChanged(float progress)
    {
        descriptionText.text = $"{chore.Type.description} {progress * 100:F0}%";
    }

    private void Chore_Completed(ChoreData chore)
    {
        Dispose();
        Destroy(gameObject);
    }

    void Dispose()
    {
        chore.Completed -= Chore_Completed;
        chore.ProgressChanged -= Chore_ProgressChanged;
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
