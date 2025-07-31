using UnityEngine;

public class Leaf : MonoBehaviour
{
    public event System.Action<Leaf> Destroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyArea"))
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
