using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] string powerUpName;
    // Define the ability or effect provided by this power-up
    public void ApplyAbility(Player player, string powerUpName)
    {
        player.ActivatePowerUp(powerUpName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // Apply the power-up ability to non-healthy player
            if(player.health<1000 && this.powerUpName == "HealthUp") {
                ApplyAbility(player, this.powerUpName);
                Destroy(gameObject);
                
            } else if(this.powerUpName != "HealthUp") {
                ApplyAbility(player, this.powerUpName); 
                Destroy(gameObject);

            }
        }
    }
}
