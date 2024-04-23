using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Entity
{
    private Player player;
    public LineRenderer lineRenderer;
    private float lineDuration = 0.1f;
    private Vector3 spawnPosition;
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

        // if attack > other.defense then attack
        if (this.attack > other.defense)
        {
            Debug.Log($"{name} at {transform.position} attacks {other.name} at {other.transform.position} with Attack: {attack}");

            // call TakeDamage()
            other.TakeDamage(attack);

            // draw attack line from enemy to other
            drawLineToPlayer();
        }

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
        Destroy(gameObject);
        Debug.Log("Tutorial enemy died.");
    }

    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Enemy: {name}";
        temp += $", Spawnpoint: {spawnPosition}";
        return temp;
    }
}
