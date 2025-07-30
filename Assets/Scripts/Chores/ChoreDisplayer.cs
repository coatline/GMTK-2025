using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoreDisplayer : MonoBehaviour
{
    [SerializeField] TMP_Text textPrefab;
    [SerializeField] Transform textHolder;

    private void Start()
    {
        ChoreManager.I.ChoresUpdated += UpdateChoreDisplay;
        UpdateChoreDisplay();
    }

    private void UpdateChoreDisplay()
    {
        foreach (ChoreData chore in ChoreManager.I.AssignedChores)
        {
            TMP_Text text = Instantiate(textPrefab, textHolder);
            text.text = chore.type.name;
        }
    }
}
