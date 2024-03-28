using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world space in 2D
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a raycast in 2D from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Deal damage to the enemy
                    enemy.TakeDamage(this.attack);
                }
            }
        }
    }
    protected override void Die()
    {
        Debug.Log("Player died!");

        //here is where we will stop gamethread and show the gameoverscreen.
        stateManager.loadGameOver();
    }
}
