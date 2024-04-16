using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Entity : MonoBehaviour
{
    [SerializeField] protected int level;
    [SerializeField] public int health;
    [SerializeField] public int attack;
    [SerializeField] public int defense;
    public GameObject damageNumberPrefab;
    public void TakeDamage(int damage)
    {
        if (damage > defense)
        {
            Debug.Log("damage: " + damage + " and defense: " + defense); 
            int actualDamage = damage - defense;
            health -= (actualDamage);
            // Instantiate damage number prefab at enemy's position
            GameObject damageNumberObj = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);

            if (damageNumberPrefab != null)
            {
                // Set the damage value on the damage number
                DamageNumber damageNumber = damageNumberObj.GetComponent<DamageNumber>();

                damageNumber.SetDamage(actualDamage);
                if (health <= 0)
                {
                    Die();
                }
            }
            else
            {
                Debug.Log("did not create damage number object.");
            }
        }
    }
    protected virtual void Die()
    {
        // Override this method in derived classes
        Debug.Log("Entity died!");
    }
}