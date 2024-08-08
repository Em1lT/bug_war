using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;
    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.Euler(0, 0, Time.deltaTime * 10f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<enemy>().StartCoroutine(other.gameObject.GetComponent<enemy>().Death());  
        }
    }
}
