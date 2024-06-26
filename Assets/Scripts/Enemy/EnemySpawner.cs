using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    TextMeshProUGUI label; 

    string spawnerLabelBase;

    void Start(){
        // Find the TextMeshProUGUI component in the child objects
        label = GetComponentInChildren<TextMeshProUGUI>();

        // Set sorting layer and order to ensure visibility
        label.canvas.sortingLayerName = "Enemy"; 
        label.canvas.sortingOrder = 10; 

        // set the spawner label base
        spawnerLabelBase = spawnerLabel;
    }

    void Awake(){
        SetTimeUntilNextSpawn();
    }
    
    void Update(){
        // update time remaining
        _timeUntilNextSpawn -= Time.deltaTime;
        //Debug.Log("Time Until Next Spawn: " + _timeUntilNextSpawn);

        // Update the label every frame
        UpdateSpawnerLabel(label);

        if(_timeUntilNextSpawn <= 0 && _counter<_maxSpawnCount && _spawnerActive){

            // instantiate enemy
            GameObject enemyInstance = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

            // set time until next spawn
            SetTimeUntilNextSpawn();

            // increment counter
            _counter++;

            // enemy component
            Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();

            // if enemy not null and enemy level is zero (not specified in inspector...)
            if(enemyComponent != null && label != null){

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

            // destroy if _counter <= 0
            if(_maxSpawnCount - _counter <= 0) {
                Die();
            }

        }
    }

    private void SetTimeUntilNextSpawn(){
        _timeUntilNextSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);

    }

    private void UpdateSpawnerLabel(TextMeshProUGUI label)
    {
        // append enemy count remaining to label
        int remaining = _maxSpawnCount - _counter;

        // Round time remaining to the nearest whole number in seconds
        int secondsRemaining = Mathf.RoundToInt(_timeUntilNextSpawn);
        Debug.Log("Rounded Seconds Remaining: " + secondsRemaining);

        // concatenate label components
        spawnerLabel = $"{spawnerLabelBase} ({remaining.ToString()}) {secondsRemaining}";

        // Set the label text to the serialized spawner label
        label.text = spawnerLabel;
    }

    void Die(){
        Destroy(gameObject);
    }

}
