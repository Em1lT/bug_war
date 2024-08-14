using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer image;
    public Sprite variant1;
    public Sprite variant2;
    public Sprite variant3;
    public Sprite variant4;
    private Animator animator;
    private AnimationStates currentState;

    private enum AnimationStates {
        flower_idle,
        flower_step,
        flower_wave
    }

    void ChangeAnimationState(AnimationStates newState) {
        if(currentState == newState) return;
        animator.Play(newState.ToString());
        currentState = newState;
    }

    private IEnumerator PlayFlower(float time) {
        animator.enabled = false;
        yield return new WaitForSeconds(time);
        ChangeAnimationState(AnimationStates.flower_idle);
        animator.enabled = true;
    }

    private IEnumerator FlowerWave() {
        animator.enabled = true;
        ChangeAnimationState(AnimationStates.flower_wave);
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        // random float
        float random = Random.Range(0.5f, 2.5f);
        int randomVariant = Random.Range(0, 4);
        int randomRotation = Random.Range(1, 3);
        Debug.Log(randomRotation);
        switch(randomRotation) {
            case 1:
                transform.Rotate(0f, 180f, 0f);
                break;
            case 2:
                transform.Rotate(0f, 0f, 0f);
                break;
        }   

        image = gameObject.GetComponent<SpriteRenderer>();
        switch(randomVariant) {
            case 0:
                image.sprite = variant1;
                break;
            case 1:
                image.sprite = variant2;
                break;
            case 2:
                image.sprite = variant3;
                break;
            case 3:
                image.sprite = variant4;
                break;
        }
        // StartCoroutine(PlayFlower(random));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FlowerStep() {
        animator.enabled = true;
        ChangeAnimationState(AnimationStates.flower_step);
        yield return new WaitForSeconds(0.5f);
        ChangeAnimationState(AnimationStates.flower_idle);
        animator.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            StartCoroutine(FlowerStep());
        }
        if(other.CompareTag("Grenade")) {
            StartCoroutine(FlowerWave());
        }
    }
}
