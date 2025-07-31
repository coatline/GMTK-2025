using UnityEngine;

public class FocusState : MonoBehaviour
{
    public void SetState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.None: break;
            case CharacterState.Laptop: break;
        }
    }

    private void Update()
    {

    }
}

public enum CharacterState
{
    None,
    Laptop
}
