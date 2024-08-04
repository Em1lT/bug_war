using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float magSize = 10f;
    public GameObject objectToSpawn;
    public float speed = 1.0f;
    void Start()
    {
        
    }


    private void Shoot() {
        Instantiate(objectToSpawn, transform.position, transform.rotation);       
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

}
