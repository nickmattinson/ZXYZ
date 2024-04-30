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

                // first three are tutorials, else normal
                switch(i) 
                {

                case 0: // Tutorial easy
                    randomEnemyIndex = 0;     
                    break;

                case 1: // Tutorial easy
                    randomEnemyIndex = 0;     
                    break;


                case 2: // Tutorial med
                    randomEnemyIndex = 1;     
                    break;


                default: // all other enemy
                    randomEnemyIndex = Random.Range(0, enemies.Count);
                    break;
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
