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
            // Apply the power-up ability to the player
            ApplyAbility(player, this.powerUpName);
            // Destroy the power-up GameObject once the ability is applied
            Destroy(gameObject);
        }
    }
}
