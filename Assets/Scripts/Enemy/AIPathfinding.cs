using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathfinding : MonoBehaviour
{
    public Player player; 
    private float distanceToPlayer;
    [SerializeField] Rigidbody2D rb2dEnemy;
    private List<Enemy> enemies = new List<Enemy>();

    void Update()
    {

        // player reference
        player = FindObjectOfType<Player>();

        // Find all Enemy objects in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {

            // calc distance to player
            distanceToPlayer = Vector2.Distance(enemy.transform.position, player.transform.position);

            // decrease time to next attack by delta
            enemy.timeToNextAttack -= Time.deltaTime;

            // Check if player is within minEngagementDistance
            if (distanceToPlayer <= enemy.minEngagementDistance)
            {
                // Move towards player only if outside minManeuverDistance
                if (distanceToPlayer > enemy.minManeuverDistance)
                {
                    Vector2 direction = player.transform.position - enemy.transform.position;

                    direction.Normalize();

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);

                    enemy.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }

                if (enemy.timeToNextAttack <= 0f)
                {

                    // Attack player if within minEngagementDistance
                    enemy.Attack(player);
                    enemy.timeToNextAttack = 1 / enemy.attackRate;
                }

            }
            else if (distanceToPlayer <= enemy.minVisibilityRange)
            {
                // Move towards player but don't engage (can be a different behavior)
                Vector2 direction = player.transform.position - transform.position;

                direction.Normalize();

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else
            {
                // Stop moving if outside minVisibilityRange
                rb2dEnemy.velocity = Vector2.zero;
            }

        }
    }
}
