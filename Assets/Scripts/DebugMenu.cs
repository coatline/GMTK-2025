using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMenu : Singleton<DebugMenu>
{
    [SerializeField] GameObject visuals;
    [SerializeField] TMP_Text textPrefab;
    [SerializeField] Transform textHolder;

    Dictionary<string, TMP_Text> valuesToTrack;

    protected override void Awake()
    {
        base.Awake();
        valuesToTrack = new Dictionary<string, TMP_Text>();
    }

    public void DisplayValue(string id, GameObject gob) => DisplayValue(id, gob == null ? "null" : gob.name);
    public void DisplayValue(string id, MonoBehaviour gob) => DisplayValue(id, gob == null ? "null" : gob.name);

    public void DisplayValue(string id, string value)
    {
        if (valuesToTrack.ContainsKey(id) == false)
            valuesToTrack.Add(id, Instantiate(textPrefab, textHolder));

        valuesToTrack[id].text = $"{id}: {value}";
    }

    public void Toggle()
    {
        visuals.SetActive(!visuals.activeSelf);
    }
}
