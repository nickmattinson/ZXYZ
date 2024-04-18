using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
     private Player player;
    //[SerializeField] List<Transform> enemySpawnPoints;

     void Start(){
        player = FindObjectOfType<Player>();
        Debug.Log(this);
     }
    public void attackPlayer(){
        Debug.Log("This enemy has this attack: " + this.attack);
        player.TakeDamage(this.attack);
        
    }
    protected override void Die()
    {
        player.score += 1;
        Destroy(gameObject);
        // Implement enemy-specific death behavior here
    }

    public override string ToString()
    {
        return $"Enemy: {name}, Level: {level}, Health: {health}, Defense: {defense}, Attack: {attack}, Position: {transform.position}, Spawnpoint: {"hello"}";
    }
}
