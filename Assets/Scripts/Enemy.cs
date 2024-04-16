using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
     private Player player;

     void Start(){
        player = FindObjectOfType<Player>();
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
}
