using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image progressBar;

    public void Setup(ChoreData chore)
    {
        nameText.text = chore.type.name;
        descriptionText.text = chore.type.description;
        chore.Completed += Chore_Completed;
    }

    private void Chore_Completed()
    {

    }
}
