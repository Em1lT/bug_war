using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake() {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode() {
        Debug.Log("SExplode");
        yield return new WaitForSeconds(1f);
        Debug.Log("Explode");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
