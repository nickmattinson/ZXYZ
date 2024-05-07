using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public LayerMask foregroundLayer;
    public LineRenderer lineRenderer;

    public bool IsLineOfSightClear(Vector3 start, Vector3 end)
    {
        RaycastHit2D hit = Physics2D.Linecast(start, end, foregroundLayer);
        return hit.collider == null;
    }

    public void ShootAtEnemy(Vector3 playerPos, Vector3 mousePos)
    {
        if (IsLineOfSightClear(playerPos, mousePos))
        {
            lineRenderer.SetPosition(1, mousePos);
            //Debug.Log("Clear line of sight. Shooting...");
            // Add your shooting logic here
        }
        else
        {
            RaycastHit2D hit = Physics2D.Linecast(playerPos, mousePos, foregroundLayer);
            lineRenderer.SetPosition(1, hit.point);
            //Debug.Log($"Obstacle detected at {hit.point}. Adjusting aim...");
        }
    }
}
