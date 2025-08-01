using UnityEngine;

public class ParentSpeechController : MonoBehaviour
{
    public event System.Action SaidSomething;

    public void Say(string s)
    {
        print($"Parent says: {s}");
    }
}
