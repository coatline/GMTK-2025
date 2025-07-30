using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ObjectHolder playerHand;
    [SerializeField] Interactor interactor;

    public void LookAtLaptop()
    {
    }

    public void LeaveLaptop()
    {
    }


    public ObjectHolder PlayerHand => playerHand;
}
