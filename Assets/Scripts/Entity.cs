using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Entity : MonoBehaviour
{

    //[SerializeField] protected string name;
    [SerializeField] protected int level;
    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    [SerializeField] protected int defense;

    public void TakeDamage(int damage)
    {
        if(damage > defense){
            health -= (damage - defense);
            Debug.Log(name + "'s health is " + health);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        // Override this method in derived classes
        Debug.Log("Entity died!");
    }
}
