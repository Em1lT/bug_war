using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{

    public GameObject player;
    public movement playerScript;
    public BloodSpawner bloodSpawner;
    public PointSpawner pointSpawner;

    public float health = 100f;

    public Sprite bloody_image;
    public Sprite death_image;

    private SpriteRenderer image;

    public float rotationSpeed = 1.0f;

    private float speed;
    public bool isDead = false;

    public BoxCollider2D boxCollider;

    private Animator animator;
    private AnimationStates currentState = AnimationStates.bug_movement;
    // Start is called before the first frame update
    void Start()
    {
    }

    private enum AnimationStates {
        bug_movement,
        bug_movement_hurt,
        death
    }
    void ChangeAnimationState(AnimationStates newState) {
        if(currentState == newState) return;
        animator.Play(newState.ToString());
        currentState = newState;
    }

    void Awake() {
        speed = Random.Range(0.5f, 2.5f);
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<movement>();
        image = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        bloodSpawner = GameObject.FindGameObjectWithTag("BloodSpawner").GetComponent<BloodSpawner>();
        pointSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PointSpawner>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        ChangeAnimationState(AnimationStates.bug_movement);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && !playerScript.isDead) {        
            animator.enabled = true;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  Time.deltaTime);
            // transform.forward = Vector3.RotateTowards(transform.position, player.transform.position, speed * Time.deltaTime, 1f);
            // Calculate the direction vector from enemy to player
            Vector3 direction = player.transform.position - transform.position;

            // Normalize the direction vector to get only the direction without affecting the magnitude
            direction.Normalize();

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the rotation of the enemy to face the player
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
        }
    }

    public IEnumerator Death () {
        // play death animation
        if(!isDead) {
            speed = 0f;
            boxCollider.enabled = false;
            pointSpawner.DestroyPoint();
            isDead = true;
            ChangeAnimationState(AnimationStates.death);
            yield return new WaitForSeconds(3f);
            animator.enabled = false;
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
            float damage = other.gameObject.GetComponent<Bullet>().damage;
            health -= damage;
            if(health <= 0) {
                StartCoroutine(Death());
            } else if (health > 0 && health < 50) {
                // Switch to bloody sprite
                ChangeAnimationState(AnimationStates.bug_movement_hurt);
                image.sprite = bloody_image;
            }
            // destroy bullet. could be moved to bullet script but saves one trigger call
            Destroy(other.gameObject,0.05f);
        }
    }


}
