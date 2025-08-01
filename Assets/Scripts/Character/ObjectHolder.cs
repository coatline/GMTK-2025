using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField] Transform hand;

    HoldableObject holdableObject;

    public void Pickup(HoldableObject holdableObject)
    {
        SoundPlayer.I.PlaySound("PickupObject", transform.position);
        this.holdableObject = holdableObject;
        holdableObject.Pickup();
        holdableObject.LeftHand += HoldableObject_LeftHand;
        holdableObject.Destroyed += HoldableObject_LeftHand;
    }

    private void HoldableObject_LeftHand()
    {
        holdableObject.LeftHand -= HoldableObject_LeftHand;
        holdableObject.Destroyed -= HoldableObject_LeftHand;
        holdableObject = null;
    }

    public void TryDrop()
    {
        holdableObject.Drop(transform.forward);
        SoundPlayer.I.PlaySound("DropObject", hand.position);
    }

    public void StartUsing()
    {
        holdableObject.StartUsing(transform.forward);
    }

    public void ContinueUsing()
    {
        holdableObject.ContinueUsing(transform.forward);
    }

    public void FinishUsing()
    {
        holdableObject.FinishUsing(transform.forward);
    }

    private void Update()
    {
        if (holdableObject != null)
        {

            holdableObject.Hold(hand.position, new Vector3(holdableObject.RotateVertically ? Camera.main.transform.rotation.eulerAngles.x : 0, hand.rotation.eulerAngles.y, hand.rotation.eulerAngles.z));
        }

        DebugMenu.I.DisplayValueGob("Holding", holdableObject);
    }

    public bool HasItem => holdableObject != null;
}
