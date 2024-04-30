using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    //private float lineDuration = 0.1f;
    [SerializeField] List<GameObject> powerupList;
    [SerializeField] List<GameObject> enemyTypeList;
    [SerializeField] int chanceForPowerUp;
    private Vector3 spawnPosition;
    private Vector3 deathPosition;

    private bool respawn = true;

    private bool scoreable = true;

    //private bool loot = true;

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

        Debug.Log($"[{this.name}] {this} ____ STARTED.");
    }

    public void SetCapability(){
        player = FindObjectOfType<Player>();
        switch(GetLevel()) 
        {

        case 1: // easy - light gray
            SetAttack(2);
            SetHealth(10);
            SetDefense(3);
            SetSpriteColor(new Vector4(.88f, .88f, .88f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));       
            break;

        case 2: // med - light blue
            SetAttack(3);
            SetDefense(4);
            SetHealth(16);            
            SetSpriteColor(new Vector4(.36f, .55f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f)); 
            break;
            
        case 3: // hard - green
            SetAttack(5);  // at least
            SetDefense(4); // at least
            SetHealth(24); 
            SetSpriteColor(new Vector4(.4f, 1, .59f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 4: // super - yellow
            SetAttack(7);  // at least
            SetDefense(5); // at least
            SetHealth(30); 
            SetSpriteColor(new Vector4(.91f, .91f, .31f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 5: // mega - orange
            SetAttack(9);  // at least
            SetDefense(7); // at least
            SetHealth(36); 
            SetSpriteColor(new Vector4(1, .83f, .27f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 6: // ultra - red
            SetAttack(13);  // at least
            SetDefense(9); // at least
            SetHealth(45); 
            SetSpriteColor(new Vector4(1, 0.27f, 0.34f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 7: // boss - purple
            SetAttack(20);  // at least
            SetDefense(15); // at least
            SetHealth(60); 
            SetSpriteColor(new Vector4(.55f, .37f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        default:
            // Default 
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
        if (rn >= (100-chanceForPowerUp))
        {
            GameObject currentPowerupPrefab = powerupList[Random.Range(0, powerupList.Count)];
            GameObject powerupInstance = Instantiate(currentPowerupPrefab, deathPosition, Quaternion.identity);
            //Debug.Log($"Power up {powerupInstance} created.");
        }

        // step 3 - after random short delay, spawn new enemy at the spawn position as function of the game level and player health
        if(respawn){
            StartCoroutine(SpawnEnemyWithDelay());
            transform.position = new Vector3(1000f, 1000f, transform.position.z);

        } else {
            Destroy(gameObject);
        }
    
 

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

        if(respawn){
            float minimum = 4f; // 4 seconds
            float maximum = 10f; // 10 seconds

            // Generate a random spawn delay within the specified range
            float spawnDelay = Random.Range(minimum, maximum);

            Debug.Log($"{this.name} about to respawn Delay [{spawnDelay}].   ____RESPAWN");

            // Wait for the specified delay
            yield return new WaitForSeconds(spawnDelay);

            // Spawn the enemy after the delay
            SpawnEnemy();

            // Destroy the object
            Destroy(gameObject);
        }
    }


    public void SpawnEnemy()
    {
        Debug.Log($"{this.name}.   ____ SPAWNENEMY");

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

        Debug.Log($"[{enemyComponent.name}] {enemyComponent} ___REPLACEMENT based on {player}, Random Enemy Index: {randomEnemyIndex+1}");
    }

public void SetRespawn(bool respawn){
    this.respawn = respawn;
}

public bool GetRespawn(){
    return this.respawn;
}

    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Spawnpoint: {this.GetSpawnPosition()}";
        temp += $", Respawn: {this.GetRespawn()}";
        return temp;
    }
}
