using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Player : Entity
{
    private Jersey jersey;

    public ShootingController shootingController;
    public StateManager stateManager;
    public int score;  // should be private
    public string username;  // should be private
    private TextMeshProUGUI scoreText;

    //[SerializeField] LineRenderer lineRenderer; // Reference to LineRenderer component

    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        SetLevel(1);
        SetAttack(7);
        SetHealth(200);
        SetDefense(1);

        // set sprite color
        // then set attack color 30% darker
        SetSpriteColor(new Vector4(.72f, .81f, 1.0f, 1));
        SetAttackColor(Brighten(GetSpriteColor(), 0.5f)); 

        username = "Unknown player";
        //Debug.Log($"[{this.name}] {this} ____ AWAKE.");

    }

    public new void Start(){
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        stateManager = FindObjectOfType<StateManager>();
        this.transform.position = new Vector2(0f, 90f);

        // setup player jersey
        //jersey = FindAnyObjectByType<Jersey>();


        // set sprite color
        //SetSpriteColor(GetSpriteColor());

        //Debug.Log($"[{this.name}] {this} ____ STARTED.");

    }


    void Update()
    {

        // update jersey
        //jersey.SetJersey(this.GetHealth().ToString());

        if (Input.GetMouseButtonDown(0))
        {
            // Find the ShootingController in the scene
            ShootingController shootingController = FindObjectOfType<ShootingController>();

            if (shootingController != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        // Check if the line of sight between player and enemy is clear
                        Vector3 playerPos = transform.position;
                        Vector3 enemyPos = enemy.transform.position;

                        // Perform raycast and adjust aim if needed
                        shootingController.ShootAtEnemy(playerPos, mousePosition);

                        // If the line of sight is clear (no obstacles), then attack
                        if (shootingController.IsLineOfSightClear(playerPos, enemyPos))
                        {
                            Attack(enemy);
                        }
                    }
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
                //AttackUp(2);  // max of xx
                this.AttackUp();
                break;
            case "HealthUp":
                // code block
                //HealthUp(20); // max of xxx
                this.HealthUp();
                break;
            case "DefenseUp":
                // code block
                //DefenseUp(2);  // max of xxx
                this.DefenseUp();
                break;
            default:
                // code block
                break;
        }
    }

    protected override void Die()
    {
        Debug.Log($"[{username}] died with score [{score}]  ____SCORE");
        //scoreText.text = score.ToString();
        stateManager.loadGameOver();
        //Destroy(this);
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
        string temp = $"{base.ToString()}";
        temp += $", Score: {this.GetScore()}";
        temp += $", Username: {this.GetUsername()}";
        return temp;
    }
}
