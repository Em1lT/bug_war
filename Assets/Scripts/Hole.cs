using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    private bool shakeLeft = false;
    private bool shake = false;
    void Start()
    {
        StartCoroutine(SpawnHole());
        
    }

    IEnumerator SpawnHole() {
        shake = true;
        yield return new WaitForSeconds(3f);
        shake = false;
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        shakeLeft = !shakeLeft;
        if(shake) {
            if(shakeLeft) {
                transform.localPosition += new Vector3(-0.1f, 0, 0);
            } else {
                transform.localPosition += new Vector3(0.1f, 0, 0);
            }
        }
    }
}
