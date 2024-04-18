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

                //THIS LINE WILL BE MODIFIED LATER TO ADJUST FOR DIFFICULTY IF EVER ADDED
                GameObject currentEnemy = enemies[Random.Range(0, enemies.Count)];

                //create enemy
                Instantiate(currentEnemy, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
