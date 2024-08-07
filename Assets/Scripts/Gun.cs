using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float magSize = 10f;

    public TMP_Text magazineText;
    public GameObject objectToSpawn;
    public float speed = 1.0f;
    public bool isReloading = false;
    public AudioSource audioSource;
    public AudioClip shootSound;
    void Start()
    {
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        magazineText.text = magSize.ToString();
    }

    private void Shoot() {
        audioSource.PlayOneShot(shootSound);
        Instantiate(objectToSpawn, transform.position, transform.rotation);       
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(1f);
        magSize = 10;
        isReloading = false;
        magazineText.text = magSize.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            StartCoroutine(Reload());
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(magSize <= 0) {
                StartCoroutine(Reload());
                return;
            }

            if(!isReloading) {
                Shoot();
                magSize--;
            }
            magazineText.text = magSize.ToString();
        }
    }

}
