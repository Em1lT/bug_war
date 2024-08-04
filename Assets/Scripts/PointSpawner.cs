using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectToSpawns;
    public int maxPoints = 10;
    public float  spawnRate = 3.0f;
    public float  repeatRate = 1.0f;
    
    public float currentPoints = 0;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    void Start()
    {
        InvokeRepeating("SpawnPoints", spawnRate, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnPoints()
    {
    if( this.currentPoints < this.maxPoints ) {
        this.currentPoints++;
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );
        // get random object to spawn
        GameObject gameObject = Instantiate(objectToSpawns[Random.Range(0, objectToSpawns.Length)], randomPosition, Quaternion.identity);
        // gameObjects.Add(gameObject);
    }
    }

    public void DestroyPoint()
    {
        this.currentPoints = 0;
    }   
}
