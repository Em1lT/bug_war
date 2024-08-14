using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectToSpawns;
    public float  spawnRate = 3.0f;
    public float  repeatRate = 1.0f;
    
    public float currentPoints = 0;
    public float respawnedEnemies = 0;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public GameManager gameManager;
    private float maxPoints = 1f;
    void Start()
    {
        InvokeRepeating("SpawnPoints", spawnRate, repeatRate);
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        maxPoints = gameManager.waveNumber * 2;
        respawnedEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnPoints()
    {
    if( respawnedEnemies < maxPoints) {
        currentPoints++;
        respawnedEnemies++;
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );
        // get random object to spawn
        GameObject gameObject = Instantiate(
            objectToSpawns[Random.Range(0, objectToSpawns.Length)],
            randomPosition,
            Quaternion.Euler(0, 0, Random.Range(0, 360))
        );
        // gameObjects.Add(gameObject);
    }
    }

    public void DestroyPoint()
    {
        currentPoints--;
        if(respawnedEnemies >= maxPoints && currentPoints == 0) {
            // round is over. next round 
            gameManager.SpawnNewWave();
            Destroy(gameObject);
        }
    }   
}
