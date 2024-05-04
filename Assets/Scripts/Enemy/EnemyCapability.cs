using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapability : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void SetEnemyCapability(int _minEnemyLevel, int maxEnemyLevel, int playerLevel, int playerScore, int playerAttack, int playerDefense,
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

        // Ensure enemy level is within specified bounds
        enemyLevel = Mathf.Clamp(enemyLevel, _minEnemyLevel, maxEnemyLevel);

        // Reduce enemy attack and defense by a fixed amount
        int attackReduction = 5; // Reduce enemy attack by 5
        int defenseReduction = 2; // Reduce enemy defense by 2

        enemyAttack = Mathf.Clamp(playerAttack + (enemyLevel * 2) - attackReduction, 2, 20);
        enemyDefense = Mathf.Clamp(playerDefense + (enemyLevel * 1) - defenseReduction, 1, 15);
    }
}
