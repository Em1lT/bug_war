using System.Collections;
using System.Collections.Generic;
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

    public AudioSource audioSource;

    public float health = 100;
    bool isDashAvailable = false;
    private SpriteRenderer image;
    private Animator animator;
    private bool isCrouching = false;
    public AudioClip hurtSound;

    private enum AnimationStates {
        idle,
        dash,
        crawl
    }

    private AnimationStates currentState;

    void ChangeAnimationState(AnimationStates newState) {
        if(currentState == newState) return;
        animator.Play(newState.ToString());
        currentState = newState;
    }

    void Awake() {
        image = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        animator.Play(AnimationStates.idle.ToString());
        audioSource = GetComponent<AudioSource>();
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
                // image.sprite = jump_image;
                ChangeAnimationState(AnimationStates.crawl);
                speed = 2f;
                // anim.Stop("crawl");
                rotationSpeed = 100f;
                return;
            }
            if(isCrouching) {
                isCrouching = false;
                speed = 5f;
                // anim.Play("crawl");
                ChangeAnimationState(AnimationStates.idle);
                rotationSpeed = 500f;
                // image.sprite = run_image;
                return;
            }
    }
    private void specialKeysUpdate() {
        if(Input.GetKeyDown(KeyCode.X)) {
            crouch();
        }
        if(Input.GetKey(KeyCode.C) && isDashAvailable) {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash() {
        isDashAvailable = false;
        transform.Translate(Vector3.forward * 100f);
        image.sprite = jump_image;
        yield return new WaitForSeconds(.1f);
    }

    private IEnumerator Invincible() {
        isDashAvailable = false;
        transform.Translate(Vector3.forward * 100f);
        image.sprite = jump_image;
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point")) {
            score.AddScore(1);
            spawner.DestroyPoint();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Enemy")) {
            health -= 10;
            audioSource.PlayOneShot(hurtSound);
            // StartCoroutine(Invincible());
            Destroy(other.gameObject);
        }
    }
}
