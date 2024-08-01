using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 1.0f;

    public PointSpawner spawner;
    public float turnSpeed = 1.0f;
    public Score score;

    private Rigidbody2D rb;
    // Start is called before the first frame update

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
    float horizontalAxis = Input.GetAxis("Horizontal");
    float verticalAxis = Input.GetAxis("Vertical");
    transform.position += Vector3.right * horizontalAxis * turnSpeed * 0.01f;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point")) {
            score.AddScore(1);
            spawner.DestroyPoint();
            Destroy(other.gameObject);
        }
    }
}
