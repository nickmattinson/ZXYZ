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

    GameObject enemyRef;

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

                if (i >= 0 && i < 3)  // First three spawn points
                {
                    // Randomly select between level 1 and level 2 enemy
                    randomEnemyIndex = Random.Range(0, 2); // 0 or 1
                }
                else
                {
                    // Randomly select from all available enemies for other spawn points
                    randomEnemyIndex = Random.Range(0, enemies.Count);
                }

                // Create enemy instance
                enemyRef = (GameObject)Resources.Load("Enemy");
                GameObject enemyInstance = Instantiate(enemyRef, spawnPoint.position, spawnPoint.rotation);
                Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();

                // set player reference
                if (enemyComponent != null)
                {
                    enemyComponent.SetSpawnPosition(enemyComponent.transform.position);
                    //enemyComponent.SetPlayerReference(player);

                    if(i<3){
                        enemyComponent.SetRespawn(false);
                    }

                    // Get player reference
                    player = FindObjectOfType<Player>();

                    // Get a reference to the GameManager
                    GameManager gameManager = FindObjectOfType<GameManager>();

                    // Calculate enemy stats based on player stats
                    int playerLevel = player.GetLevel();
                    int playerScore = player.score;
                    int playerAttack = player.GetAttack();
                    int playerDefense = player.GetDefense();

                    int enemyLevel, enemyAttack, enemyDefense;
                    
                    gameManager.CalculateEnemyStats(playerLevel, playerScore, playerAttack, playerDefense,
                        out enemyLevel, out enemyAttack, out enemyDefense);

                    // Set enemy stats
                    enemyComponent.SetLevel(enemyLevel);
                    enemyComponent.SetAttack(enemyAttack);
                    enemyComponent.SetDefense(enemyDefense);

                    // set enemy color and health
                    enemyComponent.SetCapability();
                }
                else
                {
                    Debug.LogWarning("Enemy component not found on instantiated enemy!");
                }
            }
        }
    }

    // Function to calculate enemy stats based on player stats

}
