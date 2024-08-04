using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public GameObject player;

    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Death () {
        // play death animation
        // destroy enemy
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            // get damage from bullet
            // Destroy bullet
            // reduce health
            // if health <= 0 then play death animation & destroy enemy
            // leave corpse 
            health -= 10;
            if(health <= 0) {
                Death();
            } else if (health > 0 && health < 50) {
                // Switch to bloody sprite

            }
            // other.gameObject.GetComponent<ju>().DestroyBullet();
        }
    }


}
