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
        player.TakeDamage(this.attack);
        
    }
    protected override void Die()
    {
        Destroy(gameObject);
        // Implement enemy-specific death behavior here
    }
}
