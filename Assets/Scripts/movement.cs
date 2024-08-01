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
    bool isDashAvailable = false;
    private SpriteRenderer image;
    private Rigidbody2D rb;
    private bool isCrouching = false;

    public GameObject objectToSpawn;

    void Awake() {
        image = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
    movementUpdate();
    specialKeysUpdate();
    }
    private void movementUpdate() {
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
    }

    private void crouch () {
            if(!isCrouching) {
                isCrouching = true;
                speed = 2f;
                rotationSpeed = 100f;
                image.sprite = jump_image;
                return;
            }
            if(isCrouching) {
                isCrouching = false;
                speed = 5f;
                rotationSpeed = 500f;
                image.sprite = run_image;
                return;
            }
    }
    private void Shoot() {
        GameObject gameObject = Instantiate(objectToSpawn, transform.position, transform.rotation);       
    }
    private void specialKeysUpdate() {
        if(Input.GetKeyDown(KeyCode.X)) {
            crouch();
        }
        if(Input.GetKey(KeyCode.C) && isDashAvailable) {
            StartCoroutine(Dash());
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }
    private IEnumerator Dash() {
        isDashAvailable = false;
        transform.Translate(Vector3.forward * 100f);
        image.sprite = jump_image;
        yield return new WaitForSeconds(.1f);
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
