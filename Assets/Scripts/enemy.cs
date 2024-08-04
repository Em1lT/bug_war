using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public GameObject player;

    public float health = 100f;

    public Sprite bloody_image;
    public Sprite death_image;

    private SpriteRenderer image;

    public float rotationSpeed = 1.0f;

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        image = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            // transform.rotation = Quaternion.FromToRotation(player.transform.rotation, transform.rotation, rotationSpeed * Time.deltaTime);
    }

    private void Death () {
        // play death animation
        speed = 0f;
        image.sprite = death_image;

        Destroy(gameObject, 10);
        // destroy enemy
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
            // destroy bullet
            Destroy(other.gameObject,0.05f);
        }
    }


}
