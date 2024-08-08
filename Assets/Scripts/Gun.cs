using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float magSize = 10f;

    public TMP_Text magazineText;
    public GameObject bullet;
    public GameObject fullMagazine;
    public GameObject emptyMagazine;
    public GameObject flash;
    public float speed = 1.0f;
    public bool isReloading = false;

    public Vector3 bulletOffset = new Vector3(0.5f, 0f, 0f); 
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
        Vector3 bulletPosition = transform.position + transform.TransformDirection(bulletOffset);
        Instantiate(bullet, bulletPosition, transform.rotation);       
        Instantiate(flash, bulletPosition, transform.rotation);
    }
    private void SpawnMagazine() {
        if(magSize > 1) {
            GameObject mag = Instantiate(
                fullMagazine,
                transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 360)
                ));       
            mag.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * 500);
        } else {
            GameObject mag = Instantiate(
                emptyMagazine,
                transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 360)
                ));       
            mag.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * 500);
        }
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(1f);
        SpawnMagazine();
        magSize = 10;
        isReloading = false;
        magazineText.text = magSize.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isReloading) {
            StartCoroutine(Reload());
        }
        if(Input.GetKeyDown(KeyCode.Space) && !isReloading) {
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
