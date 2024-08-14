using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private AnimationStates currentState;

    private enum AnimationStates {
        flower_idle,
        flower_step,
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

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        // random float
        float random = Random.Range(0.5f, 2.5f);
        Debug.Log(random);
        StartCoroutine(PlayFlower(random));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FlowerStep() {
        ChangeAnimationState(AnimationStates.flower_step);
        yield return new WaitForSeconds(0.5f);
        ChangeAnimationState(AnimationStates.flower_idle);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            StartCoroutine(FlowerStep());
        }
    }
}
