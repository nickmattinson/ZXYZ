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
    private Enemy enemy;

    void Start()
    {
        timer = waitTime;
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
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
            //transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        if(distance <= distanceBetween){
            if (timer <= 0)
            {
                enemy.attackPlayer(transform.position);
                timer = waitTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
