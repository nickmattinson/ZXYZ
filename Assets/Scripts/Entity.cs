using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class Entity : MonoBehaviour
{
    //[SerializeField] protected string name;
    [SerializeField] protected int level;
    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    [SerializeField] protected int defense;
    public GameObject damageNumberPrefab;


    public void TakeDamage(int damage)
    {
        if(damage > defense){
            int actualDamage = damage-defense;
            health -= (actualDamage);
            //Debug.Log(name + "'s health is " + health);
            // Instantiate damage number prefab at enemy's position
            GameObject damageNumberObj = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
    
            if(damageNumberPrefab != null){
                // Set the damage value on the damage number
                DamageNumber damageNumber = damageNumberObj.GetComponent<DamageNumber>();
                //damageNumber.damageText = damageNumber.GetComponent<TextMeshPro>();
                
                damageNumber.SetDamage(actualDamage);
                if (health <= 0)
                {
                    Die();
                }
            } else {
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

//damageText = GetComponent<TextMeshPro>();