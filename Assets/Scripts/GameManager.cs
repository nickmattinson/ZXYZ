using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] GameObject easyTutorialEnemy;
    // [SerializeField] GameObject mediumTutorialEnemy;
    // [SerializeField] GameObject medium2TutorialEnemy;
    public StateManager stateManager;
    [SerializeField] GameObject player;
    protected GameObject backgroundMusic;
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        Application.targetFrameRate = 60;
        player = Instantiate(player, Vector2.zero, Quaternion.identity);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateManager.loadSettings();
        }
    }


public void CalculateEnemyStats(int _minEnemyLevel, int maxEnemyLevel, int playerLevel, int playerScore, int playerAttack, int playerDefense,
    out int enemyLevel, out int enemyAttack, out int enemyDefense)
{
    // Initialize enemy stats to default values
    enemyLevel = 1;
    enemyAttack = 2;
    enemyDefense = 1;

    // Calculate base enemy level based on player level and score
    int baseEnemyLevel = Mathf.Clamp(playerLevel + Mathf.FloorToInt(playerScore / 100), 1, 7);

    // Add a random factor for enemy level variation (+/- 1)
    enemyLevel = Mathf.Clamp(baseEnemyLevel + Random.Range(-1, 2), 1, 7);

    // ensure enemy level less than or equal to specified spawner max enemy level
    enemyLevel = Mathf.Max(Mathf.Min(enemyLevel, maxEnemyLevel),_minEnemyLevel);

    // Calculate enemy attack and defense based on player attack and defense
    enemyAttack = Mathf.Clamp(playerAttack + (enemyLevel * 2), 2, 20);
    enemyDefense = Mathf.Clamp(playerDefense + (enemyLevel * 1), 1, 15);
}
}
