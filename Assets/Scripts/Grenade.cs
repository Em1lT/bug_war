using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Explode());
    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        // slow velocity down if it's moving
        if(rb.velocity.magnitude > 0) {
            rb.velocity = rb.velocity * 0.99f;
        } 
    }
}
