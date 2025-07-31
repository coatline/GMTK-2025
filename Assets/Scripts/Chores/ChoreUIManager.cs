using UnityEngine;

public class ChoreUIManager : MonoBehaviour
{
    [SerializeField] ChoreUI choreUIPrefab;
    [SerializeField] Transform choreUIHolder;

    private void Start()
    {
        foreach (ChoreData chore in ChoreManager.I.AssignedChores)
        {
            ChoreUI ui = Instantiate(choreUIPrefab, choreUIHolder);
            ui.Setup(chore);
        }
    }
}
