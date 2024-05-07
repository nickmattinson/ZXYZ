using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask foregroundLayer;
    public LineRenderer lineRenderer;

    public float shootInterval = 1f; // Interval between shots
    private float shootTimer = 0f; // Timer to control shooting intervals

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform; // Find the player transform
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Ensure the line renderer is initially disabled
    }

    private void Update()
    {
        if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = shootInterval; // Reset the shoot timer
        }
        else
        {
            shootTimer -= Time.deltaTime; // Countdown the shoot timer
        }
    }

    private void ShootAtPlayer()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = playerTransform.position;

        if (!IsLineOfSightClear(enemyPosition, playerPosition))
        {
            // Adjust aim if there's a wall in the way
            RaycastHit2D hit = Physics2D.Linecast(enemyPosition, playerPosition, foregroundLayer);
            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, playerPosition); // Set line renderer endpoint to player if no obstacles
        }

        lineRenderer.SetPosition(0, enemyPosition); // Set line renderer start point
        lineRenderer.enabled = true; // Enable the line renderer for visual feedback

        // Perform shooting logic here based on line of sight
        // For example, instantiate a projectile or call a method to damage the player
    }

    private bool IsLineOfSightClear(Vector3 start, Vector3 end)
    {
        RaycastHit2D hit = Physics2D.Linecast(start, end, foregroundLayer);
        return hit.collider == null;
    }
}
