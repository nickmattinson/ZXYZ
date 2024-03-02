using System.Collections.Specialized;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; // Reference to the player's transform
    [SerializeField] Vector3 offset; // Offset from the target position
    [SerializeField] float smoothSpeed; // Smoothing factor for camera movement

    private Vector3 desiredPosition;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        // Check if the target (player) is valid
        if (target != null)
        {
            // Calculate the desired position for the camera
            desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position using lerp
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
    }
}