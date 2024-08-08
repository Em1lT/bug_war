using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class flash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Show());
    }

    private IEnumerator Show() {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
