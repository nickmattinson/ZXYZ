using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;
    public LineRenderer lineRenderer;
    private float lineDuration = 0.1f;
    //[SerializeField] List<Transform> enemySpawnPoints;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        Debug.Log(this);
    }
    public void attackOther(Entity other)
    {
        //Debug.Log(enemyPosition);
        Debug.Log($"{name} at {transform.position} attacks {other.name} at {other.transform.position} with Attack: {attack}");
        other.TakeDamage(attack);
        //drawLineToPlayer(enemyPosition);

    }
    public void drawLineToPlayer(Vector3 enemyPosition)
    {
        // Set the positions for the LineRenderer (start and end points)
        lineRenderer.SetPosition(0, enemyPosition); // Start position: enemy's position
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
        temp += $", Spawnpoint: {"TBD"}";
        return temp;
    }
}
