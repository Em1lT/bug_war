using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    private bool shakeLeft = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shakeLeft = !shakeLeft;
        if(shakeLeft) {
            transform.localPosition += new Vector3(-5f, 0, 0);
        }
        else {
            transform.localPosition += new Vector3(5f, 0, 0);
        }
    }
}
