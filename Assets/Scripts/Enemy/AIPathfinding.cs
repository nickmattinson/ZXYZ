using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathfinding : MonoBehaviour
{
    public Player player; // Reference to the player prefab
    //[SerializeField] float speed;
    //[SerializeField] float distanceBetween;
    //[SerializeField] float minimumDistance;
    private float distanceToPlayer;
    [SerializeField] Rigidbody2D rb2dEnemy;
    //[SerializeField] float waitTime;
    private float timer = 0f;
    private List<Enemy> enemies = new List<Enemy>(); // List to store active Enemy objects

    void Start()
    {
        // player reference
        player = FindObjectOfType<Player>();

    }

    void Update()
    {
        // distance = Vector2.Distance(transform.position, player.transform.position);
        // Vector2 direction = player.transform.position - transform.position;
        // direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // if (distance <= distanceBetween && distance >= minimumDistance)
        // {
        //     transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //     if(isrotating){
        //         transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //     }
        // }
        // else
        // {
        //     rb2dEnemy.velocity = Vector2.zero;
        // }

        // step 1 find all active enemies with visibility range
        FindActiveEnemies();


        foreach (Enemy enemy in enemies)
        {

            distanceToPlayer = Vector2.Distance(enemy.transform.position, player.transform.position);

            // Check if player is within minEngagementDistance
            if (distanceToPlayer <= enemy.minEngagementDistance)
            {
                // Move towards player only if outside minManeuverDistance
                if (distanceToPlayer > enemy.minManeuverDistance)
                {
                    Vector2 direction = player.transform.position - transform.position;
                    direction.Normalize();
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }

                // Attack player if within minEngagementDistance
                enemy.Attack(player);
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
                rb2dEnemy.velocity = Vector2.zero; // Stop moving if outside minVisibilityRange
            }

        }
    }


    void FindActiveEnemies()
    {
        // Find all Enemy objects in the scene
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        // Clear the current list of enemies
        enemies.Clear();

        // Loop through each enemy to check if it's within range of the player
        foreach (Enemy enemy in allEnemies)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.transform.position);
            if (distanceToPlayer <= enemy.minVisibilityRange)
            {
                enemies.Add(enemy); // Add the enemy to the list if it's within range
            }
        }
    }
}
