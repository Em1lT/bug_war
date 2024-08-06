using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target; // The target that the camera will follow
    public float smoothTime = 0.3f; // The time it takes to smooth the movement
    public Vector3 offset; // The offset from the target's position

    private Vector3 velocity = Vector3.zero;
    private float initialZ;

    void Start()
    {
        // Store the initial Z position of the camera
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Define the target position with the offset, keeping the initial Z position
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, initialZ);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
