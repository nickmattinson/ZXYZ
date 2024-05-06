using UnityEngine;
using TMPro;

public class SpawnerLabel : MonoBehaviour
{
    public TextMeshProUGUI label; 
    public EnemySpawner enemySpawner;

    void Start()
    {
        // Find the TextMeshProUGUI component in the child objects
        label = GetComponentInChildren<TextMeshProUGUI>();

        // Find the EnemySpawner object
        enemySpawner = FindObjectOfType<EnemySpawner>();

        if (label != null && enemySpawner != null)
        {
            // Set the label text to the serialized spawner label
            label.text = enemySpawner.spawnerLabel;

            // Set sorting layer and order to ensure visibility
            label.canvas.sortingLayerName = "Enemy"; 
            label.canvas.sortingOrder = 10; 

            // Update the spawner label in EnemySpawner script
            //enemySpawner.UpdateSpawnerLabel("hello world");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component or EnemySpawner not found!");
        }
    }
}
