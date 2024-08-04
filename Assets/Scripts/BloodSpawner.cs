using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodSpawner : MonoBehaviour
{

    public GameObject[] objectToSpawns;
    // list of spawned objects
    public List<GameObject> spawnedObjects = new List<GameObject>(100);

    public int MaxRenderedObjects = 5;
    // Start is called before the first frame update
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
        spawnedObjects.Add(spawnedObject);
        if(spawnedObjects.Count > MaxRenderedObjects) {
            // remove the first object
            spawnedObjects.RemoveAt(0);
        }
    }
}
