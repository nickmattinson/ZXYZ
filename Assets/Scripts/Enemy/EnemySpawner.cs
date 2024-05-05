using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _minSpawnTime;

    [SerializeField] private float _maxSpawnTime;

    [SerializeField] private int _minEnemyLevel = 1;

    [SerializeField] private int _maxEnemyLevel = 7;

    [SerializeField] private int _maxSpawnCount = 3;

    [SerializeField] private bool _spawnerActive = true;

    public string spawnerLabel;

    private EnemyCapability enemyCapability;

    private float _timeUntilNextSpawn;

    private int _counter = 0;

    void Awake(){
        SetTimeUntilNextSpawn();
    }
    
    void Update(){
        _timeUntilNextSpawn -= Time.deltaTime;

        if(_timeUntilNextSpawn <= 0 && _counter<_maxSpawnCount && _spawnerActive){

            // instantiate enemy
            GameObject enemyInstance = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

            // increment counter
            _counter++;

            // enemy component
            Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();

            // if enemy not null and enemy level is zero (not specified in inspector...)
            if(enemyComponent != null){

                // Get player reference
                Player player = FindObjectOfType<Player>();

                // Get a reference to the GameManager
                enemyCapability = FindObjectOfType<EnemyCapability>();

                // Calculate enemy stats based on player stats
                int playerLevel = player.GetLevel();
                int playerScore = player.score;
                int playerAttack = player.GetAttack();
                int playerDefense = player.GetDefense();

                int enemyLevel, enemyAttack, enemyDefense;
                
                enemyCapability.SetEnemyCapability(_minEnemyLevel, _maxEnemyLevel, playerLevel, playerScore, playerAttack, playerDefense,
                    out enemyLevel, out enemyAttack, out enemyDefense);

                // Set enemy stats
                enemyComponent.SetLevel(enemyLevel);
                enemyComponent.SetAttack(enemyAttack);
                enemyComponent.SetDefense(enemyDefense);
                enemyComponent.SetCapability(); // set remaining attributes

            } 


            // set time until next spawn
            SetTimeUntilNextSpawn();
        }
    }

    private void SetTimeUntilNextSpawn(){
        _timeUntilNextSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);

    }

    public void UpdateSpawnerLabel(string newLabel)
    {
        spawnerLabel = newLabel;
    }

}
