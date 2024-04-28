using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] StateManager stateManager;
    [SerializeField] List<GameObject> enemies;
    protected int numberOfSpawns;
    private Player player;

    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();

        if (stateManager.gameEnded != true)
        {
            numberOfSpawns = enemySpawnPoints.Count;
            for (int i = 0; i < numberOfSpawns; i++)
            {
                //retrieving spawn point 
                Transform spawnPoint = enemySpawnPoints[i];

                // Randomly select an enemy prefab
                int randomEnemyIndex = Random.Range(0, enemies.Count);
                GameObject currentEnemyPrefab = enemies[randomEnemyIndex];

                // Create enemy instance
                GameObject enemyInstance = Instantiate(currentEnemyPrefab, spawnPoint.position, spawnPoint.rotation);

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
            }
        }
    }
}
