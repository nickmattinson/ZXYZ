using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform playerTransform;
    
    public Transform enemyTransform;
    public LayerMask foregroundLayer;
    public LineRenderer lineRenderer;


    public bool IsLineOfSightClear()
    {
        // Perform a raycast from player to enemy to check for obstacles
        RaycastHit2D hit = Physics2D.Linecast(playerTransform.position, enemyTransform.position, foregroundLayer);
        
        // If the raycast hits something, there's an obstacle in the way
        return hit.collider == null;
    }

    public void ShootAtEnemy(Vector3 playerPos, Vector3 mousePos)
    {
        //Debug.Log($"{playerPos} - {mousePos}______SHOOTER");
        RaycastHit2D hit = Physics2D.Linecast(playerPos, mousePos, foregroundLayer);

        if (hit.collider != null)
        {
            Debug.Log($"hit.collider not null: {hit.point}______OBSTACLE");
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Debug.Log($"hit.collider null: {hit}______CLEAR LINE OF SIGHT");
            lineRenderer.SetPosition(1, mousePos);
        }
    }

}
