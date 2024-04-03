using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLineToMouse : MonoBehaviour
{
    public Player player; // Reference to the player's Transform component
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    private float lineDuration = 0.1f;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z coordinate is zero (2D space)

            // Set the playerPosition to the player's position
            Vector3 playerPosition = player.transform.position;

            // Set the positions for the LineRenderer (start and end points)
            lineRenderer.SetPosition(0, playerPosition);
            lineRenderer.SetPosition(1, mousePosition);

            // Enable the LineRenderer to make the line visible
            lineRenderer.enabled = true;

            // Start coroutine to disable LineRenderer after duration
            StartCoroutine(DisableLineRendererAfterDelay());
        }
    }
    // Coroutine to disable LineRenderer after specified duration
    private IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
}