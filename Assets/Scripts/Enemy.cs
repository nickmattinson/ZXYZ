using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    public LineRenderer lineRenderer;
    private float lineDuration = 0.1f;
    //[SerializeField] List<Transform> enemySpawnPoints;

    private Vector3 spawnPosition;

    private Vector3 deathPosition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        Debug.Log(this);

        // set the spawn position
        //spawnPosition = transform.position;
    }
    public void attackOther(Entity other)
    {
        
        // if attack > other.defense then attack
        if(this.attack > other.defense) {
            Debug.Log($"{name} at {transform.position} attacks {other.name} at {other.transform.position} with Attack: {attack}");

            // call TakeDamage()
            other.TakeDamage(attack);

            // draw attack line from enemy to other
            drawLineToPlayer();
        }

    }

    public void SetSpawnPosition(Vector3 spawnPosition) {
        this.spawnPosition = spawnPosition;
    }

    public void drawLineToPlayer()
    {
        // Set the positions for the LineRenderer (start and end points)
        lineRenderer.SetPosition(0, transform.position); // Start position: enemy's position
        lineRenderer.SetPosition(1, player.transform.position);    // End position: player's position

        // Enable the LineRenderer to make the line visible
        lineRenderer.enabled = true;

        // Start coroutine to disable LineRenderer after duration
        StartCoroutine(DisableLineRendererAfterDelay());
    }
    // Coroutine to disable LineRenderer after specified duration
    private IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
    public void SetPlayerReference(Player player)
    {
        this.player = player;
    }
    protected override void Die()
    {
        player.score += 1;
        Destroy(gameObject);
        // Implement enemy-specific death behavior here
    }
    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Enemy: {name}";
        temp += $", Spawnpoint: {spawnPosition}";
        return temp;
    }
}
