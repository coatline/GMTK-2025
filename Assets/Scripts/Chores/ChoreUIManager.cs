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

        TimeManager.I.NewDay += NewDay;
    }

    private void NewDay()
    {
        for (int i = choreUIHolder.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = choreUIHolder.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        foreach (ChoreData chore in ChoreManager.I.AssignedChores)
        {
            ChoreUI ui = Instantiate(choreUIPrefab, choreUIHolder);
            ui.Setup(chore);
        }
    }
}
