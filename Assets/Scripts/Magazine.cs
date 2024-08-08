using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    bool isBombTriggered = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private IEnumerator Explode() {
        if(!isBombTriggered) {
            isBombTriggered = true;
            Debug.Log("SExplode");
            yield return new WaitForSeconds(1f);
            // create explosion that has large sphere collider list all of the enemies around and destroy them
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // slow velocity down if it's moving
        if(rb.velocity.magnitude > 0) {
            rb.velocity = rb.velocity * 0.99f;
        } else {
            Destroy(gameObject, 10f);
        }
    }
}
