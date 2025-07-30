using UnityEngine;

public class Leaf : MonoBehaviour
{
    public event System.Action Destroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyArea"))
        {
            Destroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
