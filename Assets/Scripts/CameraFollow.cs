using System.Collections.Specialized;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset; // Offset from the target position
    [SerializeField] float smoothSpeed; // Smoothing factor for camera movement
    public Player player; // Reference to the player prefab
    private Vector3 desiredPosition;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position for the camera
            desiredPosition = player.transform.position + offset;

            // Smoothly move the camera towards the desired position using lerp
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
    }
}