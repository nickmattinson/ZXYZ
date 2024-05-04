using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLineToMouse : MonoBehaviour
{

    public ShootingController shootingController; // Reference to the ShootingController script

    public Player player; // Reference to the player's Transform component
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    private float lineDuration = 0.1f;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

void Update()
{
    if (Input.GetMouseButtonDown(0))
    {

        shootingController = FindAnyObjectByType<ShootingController>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 playerPosition = player.transform.position;

        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, mousePosition);

        Color customColor = player.GetAttackColor();
        lineRenderer.startColor = customColor;
        lineRenderer.endColor = customColor;

        lineRenderer.enabled = true;
        StartCoroutine(DisableLineRendererAfterDelay());

        // Check if the shootingController is not null
        if (shootingController != null)
        {
            // Call the ShootAtEnemy method of the shootingController
            shootingController.ShootAtEnemy(playerPosition, mousePosition);
        }
    }
}

    // Coroutine to disable LineRenderer after specified duration
    private IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
}