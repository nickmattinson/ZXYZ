using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    //private float lineDuration = 0.1f;
    [SerializeField] List<GameObject> powerupList;
    [SerializeField] List<GameObject> enemyTypeList;
    [SerializeField] int randomNumber;
    private Vector3 spawnPosition;
    private Vector3 deathPosition;

    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        // set enemy capability
        SetCapability();
        //Debug.Log($"[{this.name}] {this} ____ AWAKE.");



    }

    new void Start()
    {
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        // Get the Player component attached to the GameObject
        // player = GetComponent<Player>(); // player reference not found...
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player reference not found!_______ENEMY_START");
        }

        // set spawn position
        SetSpawnPosition(this.transform.position);

        //Debug.Log($"[{this.name}] {this} ____ STARTED.");
    }

    public void SetCapability(){
        switch(GetLevel()) 
        {

        case 1: // Easy (GREEN)
            SetAttack(2);
            SetHealth(5);
            SetDefense(3);
            SetSpriteColor(new Vector4(0,1,0,1));
            SetAttackColor(new Vector4(1,1,1,1));         
            break;

        case 2: // Medium (YELLOW)
            SetAttack(5);
            SetDefense(4);
            SetHealth(30);   
            SetSpriteColor(new Vector4(1,1,.2f,1));
            SetAttackColor(new Vector4(1,1,1,1));          
            break;
            
        case 3:
            // Hard (RED)
            SetAttack(8);
            SetDefense(5);
            SetHealth(50); 
            SetSpriteColor(new Vector4(1,0,0,1));
            SetAttackColor(new Vector4(1,1,1,1));                       
            break;

        default:
            // Default GREEN - EASY
            SetAttack(2);
            SetHealth(5);
            SetDefense(3);
            SetSpriteColor(new Vector4(0,1,0,1));
            SetAttackColor(new Vector4(1,1,1,1));  
            break;
        }
    }


    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }


    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }




    protected override void Die()
    {
        player = FindObjectOfType<Player>();
        //Debug.Log($"Bug Enemy: {name} Player: {player.GetScore()} _____ENEMY_DIE");
        //player.SetScore(player.GetScore()+1);
        player.score++;



        // step 1 - set death position
        deathPosition = transform.position;
        Debug.Log($"[{name}] killed by [{player.name}]. ____KILL");

        // step 2 - drop a random buf at death position randomly
        int rn = Random.Range(0, 100);
        //Debug.Log($"Random number: {randomNumber}");
        if (rn >= randomNumber)
        {
            GameObject currentPowerupPrefab = powerupList[Random.Range(0, powerupList.Count)];
            GameObject powerupInstance = Instantiate(currentPowerupPrefab, deathPosition, Quaternion.identity);
            //Debug.Log($"Power up {powerupInstance} created.");
        }

        // step 3 - after random short delay, spawn new enemy at the spawn position as function of the game level and player health
        StartCoroutine(SpawnEnemyWithDelay());
        transform.position = new Vector3(1000f, 1000f, transform.position.z);
    }

    IEnumerator SpawnEnemyWithDelay()
    /*
        Modify random spawn rate
        Randomly respawn between 4 and 10 seconds.
        Author: ChatGPT3.5
        Author: Mike M
        Modified: 23/Apr/24
    */
    {
        float minimum = 4f; // 4 seconds
        float maximum = 10f; // 10 seconds

        // Generate a random spawn delay within the specified range
        float spawnDelay = Random.Range(minimum, maximum);

        // Wait for the specified delay
        yield return new WaitForSeconds(spawnDelay);

        // Spawn the enemy after the delay
        SpawnEnemy();

        // Destroy the object (assuming this script is attached to the object you want to destroy)
        Destroy(gameObject);
    }


    public void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyTypeList.Count);
        GameObject currentEnemyPrefab = enemyTypeList[randomEnemyIndex];

        GameObject enemyInstance = Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);

        // Set player reference for the enemy
        Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();

        // set player reference
        if (enemyComponent != null)
        {
            enemyComponent.SetSpawnPosition(enemyComponent.transform.position);
            //enemyComponent.SetPlayerReference(player);
            enemyComponent.SetLevel(randomEnemyIndex+1);
            enemyComponent.SetCapability();
        }
        else
        {
            Debug.LogWarning("Enemy component not found on instantiated enemy!");
        }

        //Debug.Log($"Replace Enemy {enemyInstance} created at position {spawnPosition}.");
    }

    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Spawnpoint: {this.GetSpawnPosition()}";
        return temp;
    }
}
