using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class throw_enemy : MonoBehaviour
{

    public GameObject missile;
    public GameObject player;
    public movement playerScript;
    public BloodSpawner bloodSpawner;
    public PointSpawner pointSpawner;

    public float health = 100f;

    private SpriteRenderer image;

    public float rotationSpeed = 1.0f;

    private float speed;
    public bool isDead = false;

    public BoxCollider2D boxCollider;

    private Animator animator;
    private AnimationStates currentState = AnimationStates.throw_enemy_animation;
    // Start is called before the first frame update
    void Start()
    {
    }

    private enum AnimationStates {
        throw_enemy_animation,
        throw_enemy_animation_throw,

    }
    void ChangeAnimationState(AnimationStates newState) {
        if(currentState == newState) return;
        animator.Play(newState.ToString());
        currentState = newState;
    }

    void Awake() {
        speed = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<movement>();
        image = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        bloodSpawner = GameObject.FindGameObjectWithTag("BloodSpawner").GetComponent<BloodSpawner>();
        pointSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PointSpawner>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        InvokeRepeating("ThrowMissile", 2.5f, 2.5f);
    }

    private IEnumerator Missile() {

        Vector3 bulletOffset = new Vector3(0.5f, 0f, 0f); 
        Vector3 bulletPosition = transform.position + transform.TransformDirection(bulletOffset);
        Instantiate(missile, bulletPosition, transform.rotation);       
        yield return new WaitForSeconds(0.5f);

    }

    void ThrowMissile () {
        if(isDead) return;
        StartCoroutine(Missile());

    }
    // Update is called once per frame
    void Update()
    {
        if(!isDead && !playerScript.isDead) {        
            if(Vector2.Distance(transform.position, player.transform.position) < 10f) {
                Vector3 direction = player.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
            } else {
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
    }

    public IEnumerator Death () {
        // play death animation
        if(!isDead) {
            speed = 0f;
            animator.enabled = false;
            pointSpawner.DestroyPoint();
            isDead = true;
            yield return new WaitForSeconds(3f);
            boxCollider.enabled = false;
            bloodSpawner.SpawnBlood(transform.position);
            Destroy(gameObject, 5);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            // get damage from bullet & reduce health
            StartCoroutine(Death());
            Destroy(other.gameObject,0.05f);
        }
    }


}
