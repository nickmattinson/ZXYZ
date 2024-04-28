using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Entity
{
    private Player player;
    //public LineRenderer lineRenderer;
    //private float lineDuration = 0.1f;
    private Vector3 spawnPosition;

    new void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        Debug.Log(this);
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
