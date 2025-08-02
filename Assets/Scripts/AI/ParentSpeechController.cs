using TMPro;
using UnityEngine;

public class ParentSpeechController : MonoBehaviour
{
    public event System.Action SaidSomething;

    [SerializeField] TMP_Text text;

    public void Say(string s)
    {
        text.text = s;
    }
}
