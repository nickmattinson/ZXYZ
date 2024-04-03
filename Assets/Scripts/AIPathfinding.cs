using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathfinding : MonoBehaviour
{
    public Player player; // Reference to the player prefab
    [SerializeField] float speed;
    [SerializeField] float distanceBetween;
    private float distance;
    [SerializeField] Rigidbody2D rb2dEnemy;

    void Start()
    {
        // Instantiate the player prefab and assign a reference to the instantiated object
        player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            rb2dEnemy.velocity = Vector2.zero;
            //transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
