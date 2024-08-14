using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodSpawner : MonoBehaviour
{

    public GameObject[] objectToSpawns;
    // list of spawned objects
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlood(Vector3 position)
    {
        GameObject spawnedObject = Instantiate(objectToSpawns[Random.Range(0, objectToSpawns.Length)], position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(spawnedObject, 30f);
    }
}
