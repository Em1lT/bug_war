using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public GameObject player;
    public BloodSpawner bloodSpawner;

    public float health = 100f;

    public Sprite bloody_image;
    public Sprite death_image;

    private SpriteRenderer image;

    public float rotationSpeed = 1.0f;

    private float speed;
    public bool isDeath = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake() {
        speed = Random.Range(0.5f, 1.5f);
        player = GameObject.FindGameObjectWithTag("Player");
        image = gameObject.GetComponent<SpriteRenderer>();
        bloodSpawner = GameObject.FindGameObjectWithTag("BloodSpawner").GetComponent<BloodSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath) {        
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  Time.deltaTime);
            // transform.forward = Vector3.RotateTowards(transform.position, player.transform.position, speed * Time.deltaTime, 1f);
        }
    }

    private void Death () {
        // play death animation
        if(!isDeath) {
        speed = 0f;
        isDeath = true;
        image.sprite = death_image;
        bloodSpawner.SpawnBlood(transform.position);
        Destroy(gameObject, 5);
        // destroy enemy
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            // get damage from bullet & reduce health
            float damage = other.gameObject.GetComponent<bullet>().damage;
            health -= damage;
            if(health <= 0) {
                Death();
            } else if (health > 0 && health < 50) {
                // Switch to bloody sprite
                image.sprite = bloody_image;
            }
            // destroy bullet. could be moved to bullet script but saves one trigger call
            Destroy(other.gameObject,0.05f);
        }
    }


}
