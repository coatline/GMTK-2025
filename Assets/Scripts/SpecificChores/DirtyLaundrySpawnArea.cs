using UnityEngine;

public class DirtyLaundrySpawnArea : MonoBehaviour
{
    [SerializeField] int minSpawn;
    [SerializeField] int maxSpawn;
    [SerializeField] GameObject clothingPrefab;
    [SerializeField] Transform clothingSpawnPosition;


    public int GetSpawnAmount() => Random.Range(minSpawn, maxSpawn);
    public GameObject SpawnClothing()
    {
        return Instantiate(clothingPrefab, clothingSpawnPosition.position, Quaternion.identity);
    }
}
