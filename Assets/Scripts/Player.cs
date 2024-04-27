using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Player : Entity
{
    public StateManager stateManager;
    public int score;  // should be private

    public string username;  // should be private

    private TextMeshProUGUI scoreText;


    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.
        SetLevel(1);
        SetAttack(3);
        SetHealth(9999);
        SetDefense(2);
        username = "Unknown player";
        Debug.Log($"Player {name} awake at {this.transform.position}");

    }

    public new void Start(){
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        stateManager = FindObjectOfType<StateManager>();
        this.transform.position = new Vector2(0f,90f);
        Debug.Log($"Player {name} started at {this.transform.position}");

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
                    Debug.Log($"Player's attack is {this.GetAttack()}");
                    enemy.TakeDamage(this);
                }

                TutorialEnemy tutorialEnemy = hit.collider.GetComponent<TutorialEnemy>();
                if (tutorialEnemy != null)
                {
                    // Deal damage to the enemy
                    Debug.Log($"Player's attack is {this.GetAttack()}");
                    tutorialEnemy.TakeDamage(this);
                }
            }
        }
    }
    public void ActivatePowerUp(string powerUp)
    {
        switch(powerUp) 
        {
            case "AttackUp":
                // code block
                AttackUp(2);  // max of xx
                break;
            case "HealthUp":
                // code block
                HealthUp(20); // max of xxx
                break;
            case "DefenseUp":
                // code block
                DefenseUp(2);  // max of xxx
                break;
            default:
                // code block
                break;
        }
    }

    protected override void Die()
    {
        Debug.Log($"Player {username} has died. Score: {score}");
        //scoreText.text = score.ToString();
        stateManager.loadGameOver();
    }

    public void SetScore(int score){
        this.score = score;
    }

    public int GetScore(){
        return this.score;
    }


    public void SetUsername(string username){
        this.username = username;
    }

    public string GetUsername(){
        return username;
    }

    public override string ToString()
    {
        return $"{username}, Level: {this.GetLevel()}, Health: {this.GetHealth()}, Defense: {this.GetDefense()}, Attack: {this.GetAttack()}";
    }
}
