using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnerBox;
    
    [SerializeField] private int numberOfTreesToSpawn;
    [SerializeField] private GameObject objectToSpawn;

    [SerializeField] private float spawningCentreDeadZone;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfTreesToSpawn; i++)
        {
            SpawnTree();
        }
        
        spawnerBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTree()
    {
        Collider spawnZone = spawnerBox.GetComponent<Collider>();
        Vector3 randomPosition = GetRandomPositionFromSpawnZone(spawnZone);
        
        float randomHeading = Random.Range(0f, 360f);
        Vector3 randomRotationEuler = new Vector3(0, randomHeading, 0);
        
        Instantiate(objectToSpawn, randomPosition, Quaternion.Euler(randomRotationEuler));
    }

    private Vector3 GetRandomPositionFromSpawnZone(Collider spawnZone)
    {
        
        float randomX = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        float randomZ = Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z);
        
        return new Vector3(randomX , 0, randomZ);
        
        return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
}
