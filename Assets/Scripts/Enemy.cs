using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    public LineRenderer lineRenderer;
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

        SetLevel(1);
        SetAttack(3);
        SetHealth(10);
        SetDefense(2);
        //Debug.Log($"[{this.name}] {this} ____ AWAKE.");

    }

    new void Start()
    {
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        // set player reference
        player = FindObjectOfType<Player>();
        SetPlayerReference(player);

        // set spawn position
        SetSpawnPosition(this.transform.position);

        Debug.Log($"[{this.name}] {this} ____ STARTED.");
    }


    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }


    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }


    public void SetPlayerReference(Player player)
    {
        this.player = player;
    }


    protected override void Die()
    {
        player.score += 1;

        // step 1 - set death position
        deathPosition = transform.position;
        Debug.Log($"Enemy {name} was killed by player {player.name} at position {deathPosition}.");

        // step 2 - drop a random buf at death position randomly
        int rn = Random.Range(0, 100);
        //Debug.Log($"Random number: {randomNumber}");
        if (rn >= randomNumber)
        {
            GameObject currentPowerupPrefab = powerupList[Random.Range(0, powerupList.Count)];
            GameObject powerupInstance = Instantiate(currentPowerupPrefab, deathPosition, Quaternion.identity);
            Debug.Log($"Power up {powerupInstance} created.");
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
        GameObject currentEnemyPrefab = enemyTypeList[Random.Range(0, enemyTypeList.Count)];
        GameObject enemyInstance = Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);

        // Set player reference for the enemy
        Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();

        // set player reference
        if (enemyComponent != null)
        {
            enemyComponent.SetSpawnPosition(enemyComponent.transform.position);
            enemyComponent.SetPlayerReference(player);
        }
        else
        {
            Debug.LogWarning("Enemy component not found on instantiated enemy!");
        }

        Debug.Log($"Replace Enemy {enemyInstance} created at position {spawnPosition}.");
    }

    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Enemy: {name}";
        temp += $", Spawnpoint: {spawnPosition}";
        return temp;
    }
}
