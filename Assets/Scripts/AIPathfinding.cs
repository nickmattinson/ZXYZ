using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathfinding : MonoBehaviour
{
    public Player player; // Reference to the player prefab
    [SerializeField] float speed;
    [SerializeField] float distanceBetween;
    [SerializeField] float minimumDistance;
    private float distance;
    [SerializeField] Rigidbody2D rb2dEnemy;
    [SerializeField] float waitTime;
    private float timer = 0f;
    private List<Enemy> enemies = new List<Enemy>(); // List to store active Enemy objects
    private bool isrotating = true;

    void Start()
    {
        timer = waitTime;
        player = FindObjectOfType<Player>();
        FindActiveEnemies(); // Find all active Enemy objects at the start
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance <= distanceBetween && distance >= minimumDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if(isrotating){
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }
        else
        {
            rb2dEnemy.velocity = Vector2.zero;
        }

        // Check each enemy in the list
        FindActiveEnemies();
        foreach (Enemy enemy in enemies)
        {
            if (distance <= distanceBetween)
            {
                if (timer <= 0)
                {
                    enemy.Attack(player);
                    timer = waitTime;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }


public void OnCollisionEnter(Collision collision){
    isrotating = ! collision.gameObject.CompareTag("Player");
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
        if (distanceToPlayer <= distanceBetween)
        {
            enemies.Add(enemy); // Add the enemy to the list if it's within range
        }
    }
}
}
