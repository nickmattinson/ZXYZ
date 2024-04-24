using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    public LineRenderer lineRenderer;
    private float lineDuration = 0.1f;
    [SerializeField] List<GameObject> powerupList;
    [SerializeField] List<GameObject> enemyTypeList;
    [SerializeField] int randomNumber;
    private Vector3 spawnPosition;
    private Vector3 deathPosition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        Debug.Log(this);

        // set the spawn position
        //spawnPosition = transform.position;
    }
    public void attackOther(Entity other)
    {

        // if attack > other.defense then attack
        if (this.attack > other.defense)
        {
            Debug.Log($"{name} at {transform.position} attacks {other.name} at {other.transform.position} with Attack: {attack}");

            // call TakeDamage()
            other.TakeDamage(attack);

            // draw attack line from enemy to other
            drawLineToPlayer();
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

    public void drawLineToPlayer()
    {
        // Set the positions for the LineRenderer (start and end points)
        lineRenderer.SetPosition(0, transform.position); // Start position: enemy's position
        lineRenderer.SetPosition(1, player.transform.position);    // End position: player's position

        // Enable the LineRenderer to make the line visible
        lineRenderer.enabled = true;

        // Start coroutine to disable LineRenderer after duration
        StartCoroutine(DisableLineRendererAfterDelay());
    }
    // Coroutine to disable LineRenderer after specified duration
    private IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
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
