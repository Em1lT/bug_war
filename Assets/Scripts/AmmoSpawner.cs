using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{

    public float  spawnRate = 30.0f;
    public float  repeatRate = 30.0f;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public GameObject[] objectToSpawns;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPoints", spawnRate, repeatRate);
        
    }

    void SpawnPoints()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        Instantiate(
            objectToSpawns[Random.Range(0, objectToSpawns.Length)],
            randomPosition,
            Quaternion.Euler(0, 0, Random.Range(0, 360))
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
