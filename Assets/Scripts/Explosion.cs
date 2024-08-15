using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public Rigidbody2D rb;
    public CircleCollider2D bc;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }

    private IEnumerator Explode() {
        audioSource.PlayOneShot(explosionSound);
        yield return new WaitForSeconds(0.1f);
        bc.enabled = false;
        bc.isTrigger = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 10f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<enemy>().StartCoroutine(other.gameObject.GetComponent<enemy>().Death());  
        }
    }
}
