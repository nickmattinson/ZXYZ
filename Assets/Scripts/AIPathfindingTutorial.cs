using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathfindingTutorial : MonoBehaviour
{
    public Player player; // Reference to the player prefab
    [SerializeField] float speed;
    [SerializeField] float distanceBetween;
    [SerializeField] float minimumDistance;
    private float distance;
    [SerializeField] Rigidbody2D rb2dEnemy;
    [SerializeField] float waitTime;
    private float timer = 0f;
    private List<TutorialEnemy> enemies = new List<TutorialEnemy>(); // List to store active TutorialEnemy objects

    void Start()
    {
        timer = waitTime;
        player = FindObjectOfType<Player>();
        FindActiveEnemies(); // Find all active TutorialEnemy objects at the start
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
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            rb2dEnemy.velocity = Vector2.zero;
        }

        // Check each enemy in the list
        FindActiveEnemies();
        foreach (TutorialEnemy enemy in enemies)
        {
            if (distance <= distanceBetween)
            {
                if (timer <= 0)
                {
                    enemy.Attack(player, Color.white);
                    timer = waitTime;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }

    void FindActiveEnemies()
    {
        // Find all TutorialEnemy objects in the scene
        TutorialEnemy[] allEnemies = FindObjectsOfType<TutorialEnemy>();

        // Clear the current list of enemies
        enemies.Clear();

        // Loop through each enemy to check if it's within range of the player
        foreach (TutorialEnemy enemy in allEnemies)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.transform.position);
            if (distanceToPlayer <= distanceBetween)
            {
                enemies.Add(enemy); // Add the enemy to the list if it's within range
            }
        }
    }
}
