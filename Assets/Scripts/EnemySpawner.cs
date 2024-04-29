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
        int randomEnemyIndex;

        if (stateManager.gameEnded != true)
        {
            numberOfSpawns = enemySpawnPoints.Count;
            for (int i = 0; i < numberOfSpawns; i++)
            {
                //retrieving spawn point 
                Transform spawnPoint = enemySpawnPoints[i];

                // first three are tutorials, else normal
                switch(i) 
                {

                case 0: // Tutorial
                    randomEnemyIndex = 0;     
                    break;

                case 1: // Tutorial
                    randomEnemyIndex = 1;     
                    break;


                case 2: // Tutorial
                    randomEnemyIndex = 1;     
                    break;


                default: // all other enemy
                    randomEnemyIndex = Random.Range(2, enemies.Count);
                    break;
                }

                // Randomly select an enemy prefab
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
