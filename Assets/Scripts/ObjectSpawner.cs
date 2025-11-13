using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnerBox;
    
    [SerializeField] private int numberOfObjectsToSpawn;
    [SerializeField] private GameObject treeToSpawn;
    [SerializeField] private GameObject stoneToSpawn;
    

    [SerializeField] private float spawningCentreDeadZone;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            SpawnObject();
        }
        
        spawnerBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObject()
    {
        Collider spawnZone = spawnerBox.GetComponent<Collider>();
        Vector3 randomPosition = GetRandomPositionFromSpawnZone(spawnZone);
        
        float randomHeading = Random.Range(0f, 360f);
        Vector3 randomRotationEuler = new Vector3(0, randomHeading, 0);

        int random = Random.Range(0, 3);

        if (random <= 1)
        {
            Instantiate(treeToSpawn, randomPosition, Quaternion.Euler(randomRotationEuler));
        }
        else if (random >= 2)
        {
            GameObject stoneCreated =  Instantiate(stoneToSpawn, randomPosition, Quaternion.Euler(randomRotationEuler));
            float randomScale = Random.Range(0.75f, 8f);
            Vector3 randomScaleVector = new Vector3(randomScale, randomScale, randomScale);
            stoneCreated.transform.localScale = randomScaleVector;
        }
        
        
    }

    private Vector3 GetRandomPositionFromSpawnZone(Collider spawnZone)
    {
        
        float randomX = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        float randomZ = Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z);
        
        return new Vector3(randomX , 0, randomZ);
        
        return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
}
