using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movement : MonoBehaviour
{
    public float speed = 1.0f;

    public PointSpawner spawner;
    public float turnSpeed = 1.0f;
    public Score score;
    public float rotationSpeed = 1.0f;
    public Sprite jump_image;
    public Sprite grenade_image;
    public Sprite run_image;

    public TMP_Text grenadesText;

    public AudioSource audioSource;

    public float health = 100;
    bool isDashAvailable = false;
    private SpriteRenderer image;
    private Animator animator;
    private bool isCrouching = false;
    public AudioClip hurtSound;
    public float dashSpeed = 20f;      // The speed of the dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTime;
    public bool invincible = false;
    public bool grenadeReady = false;
    public GameObject grenadePrefab;

    public float numberOfGrenades = 3;
    public bool isDead = false;

    public BloodSpawner bloodSpawner;
    private GameManager gameManager;

    private enum AnimationStates {
        idle,
        crawl
    }

    private AnimationStates currentState = AnimationStates.idle;

    void ChangeAnimationState(AnimationStates newState) {
        if(currentState == newState) return;
        animator.Play(newState.ToString());
        currentState = newState;
    }

    void Awake() {
        image = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        animator.Play(AnimationStates.idle.ToString());
        audioSource = GetComponent<AudioSource>();
        bloodSpawner = GameObject.FindGameObjectWithTag("BloodSpawner").GetComponent<BloodSpawner>();
    }
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
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
            if(grenadeReady) {
                animator.enabled = true;
            }
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } else {
            if(grenadeReady) {
                animator.enabled = false;
            }
        }
    }

    private IEnumerator ThrowGrenade() {
        if(numberOfGrenades > 0) {
            numberOfGrenades--;
            grenadesText.text = numberOfGrenades.ToString();
            grenadeReady = true;
            image.sprite = grenade_image;
            yield return new WaitForSeconds(0.1f);
            image.sprite = run_image;
            grenadeReady = false;
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
            grenade.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * 1000);
        }
    }   
    private void crouch () {
            if(!isCrouching) {
                grenadeReady = false;
                isCrouching = true;
                image.sprite = jump_image;
                ChangeAnimationState(AnimationStates.crawl);
                animator.enabled = false;
                speed = 2f;
                rotationSpeed = 100f;
                return;
            }
            if(isCrouching && !grenadeReady) {
                isCrouching = false;
                speed = 5f;
                image.sprite = run_image;
                ChangeAnimationState(AnimationStates.idle);
                rotationSpeed = 500f;
                return;
            }
    }
    private void specialKeysUpdate() {
        if(Input.GetKeyDown(KeyCode.V)) {
            StartCoroutine(ThrowGrenade());
        }
        if(Input.GetKeyDown(KeyCode.X)) {
            crouch();
        }
       // if (Input.GetKeyDown(KeyCode.C) && !isDashing)
       //  {
       //      if(isCrouching) return;
       //      StartDash();
       //  }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }
    private IEnumerator Invincible() {
        // isDashAvailable = false;
        invincible = true;
        // transform.Translate(Vector3.forward * 100f);
        yield return new WaitForSeconds(1f);

        invincible = false;
        // image.sprite = jump_image;
    }
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        rb.velocity = Vector3.forward * dashSpeed;
    }

    void EndDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero; // Stop the player after the dash
    }
    void Death()
    {
        isDead = true;
        bloodSpawner.SpawnBlood(transform.position);
        gameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isDead) return;
        if (other.CompareTag("Point")) {
            score.AddScore(1);
            spawner.DestroyPoint();
            Destroy(other.gameObject);
        } else if (other.CompareTag("Enemy") && !invincible && !other.gameObject.GetComponent<enemy>().isDead) {
            health -= 100;
            StartCoroutine(Invincible());
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(hurtSound);
            }
            if(health <= 0) {
                Death();
            }
            Destroy(other.gameObject);
        }
        if(other.CompareTag("GrenadeBox")) {
            numberOfGrenades += 3;
            grenadesText.text = numberOfGrenades.ToString();
            Destroy(other.gameObject);
            
        }

    }
}
