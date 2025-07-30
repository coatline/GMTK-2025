using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] Camera cam;

    HoldableObject holdableObject;

    public void Pickup(HoldableObject holdableObject)
    {
        //SoundPlayer.I.PlaySound(pickupSound, transform.position);
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

    public void Drop()
    {
        holdableObject.Drop(cam.transform.forward);
    }

    public void FinishUsing()
    {
        holdableObject.FinishUsing(cam.transform.forward);
    }

    public void ContinueUsing()
    {
        holdableObject.ContinueUsing(cam.transform.forward);
    }

    public void StartUsing()
    {
        holdableObject.StartUsing(cam.transform.forward);
    }

    private void FixedUpdate()
    {
        if (holdableObject != null)
            holdableObject.Hold(hand.position, new Vector3(0, hand.rotation.eulerAngles.y, hand.rotation.eulerAngles.z));

        DebugMenu.I.DisplayValue("Holding", holdableObject);
    }

    public bool HasItem => holdableObject != null;
}
