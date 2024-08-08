using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // slow velocity down if it's moving
        if(rb.velocity.magnitude > 0) {
            rb.velocity = rb.velocity * 0.99f;
        } else {
            StartCoroutine(Explode());
        }
    }
}
