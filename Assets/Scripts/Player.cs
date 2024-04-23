using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public StateManager stateManager;
    public int score;

    void Start()
    {
        Vector2 vector2d = new Vector2();
        vector2d.x = 0f;
        vector2d.y = 90f;
        stateManager = FindObjectOfType<StateManager>();
        this.transform.position = vector2d;

        Debug.Log(this);
    }

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
                    Debug.Log("Player's attack is " + attack);
                    enemy.TakeDamage(this.attack);
                }

                TutorialEnemy tutorialEnemy = hit.collider.GetComponent<TutorialEnemy>();
                if (tutorialEnemy != null)
                {
                    // Deal damage to the enemy
                    Debug.Log("Player's attack is " + attack);
                    tutorialEnemy.TakeDamage(this.attack);
                }
            }
        }
    }
    public void ActivatePowerUp(string powerUp)
    {
        if (powerUp == "AttackUp")
        {
            this.attack += 2;
        }
        if (powerUp == "HealthUp")
        {

            // check no more than 100
            this.health += 100;

            if (this.health > 1000)
            {
                this.health = 1000;
            }

        }

        if (powerUp == "DefenseUp")
        {

            // check no more than 100
            this.defense += 2;

        }
    }

    protected override void Die()
    {
        Debug.Log("Player died!");
        stateManager.loadGameOver();
    }


    public override string ToString()
    {
        return $"Player: {name}, Level: {level}, Health: {health}, Defense: {defense}, Attack: {attack}";
    }
}
