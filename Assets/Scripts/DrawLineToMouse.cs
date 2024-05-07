using UnityEngine;
using System.Collections;

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
            shootingController = FindObjectOfType<ShootingController>();
            if (shootingController != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector3 playerPosition = player.transform.position;

                lineRenderer.SetPosition(0, playerPosition);

                // Check if line of sight is clear before setting the end position
                Vector3 endPosition = mousePosition;
                if (!shootingController.IsLineOfSightClear(playerPosition, mousePosition))
                {
                    RaycastHit2D hit = Physics2D.Linecast(playerPosition, mousePosition, shootingController.foregroundLayer);
                    endPosition = hit.point;
                }

                lineRenderer.SetPosition(1, endPosition);

                Color customColor = player.GetAttackColor();
                lineRenderer.startColor = customColor;
                lineRenderer.endColor = customColor;

                lineRenderer.enabled = true;
                StartCoroutine(DisableLineRendererAfterDelay());

                // Call the ShootAtEnemy method of the shootingController
                shootingController.ShootAtEnemy(playerPosition, endPosition);
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
