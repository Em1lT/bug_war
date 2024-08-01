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
    public float rotationSpeed = 1.0f;
    public Sprite jump_image;
    public Sprite run_image;
    private SpriteRenderer image;

    void Awake() {
        image = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
    float horizontalAxis = Input.GetAxis("Horizontal");
    float verticalAxis = Input.GetAxis("Vertical");

    Vector2 movement = new Vector2(horizontalAxis, verticalAxis);
    float magnitude = Mathf.Clamp01(movement.magnitude);
    movement.Normalize();

    transform.Translate(movement * speed * magnitude * Time.deltaTime, Space.World);

    if(movement != Vector2.zero) {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    if(Input.GetKeyDown(KeyCode.Z)) {
        image.sprite = jump_image;
    } if(Input.GetKeyUp(KeyCode.Z)) {
        image.sprite = run_image;
    }

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
