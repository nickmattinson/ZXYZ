using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
[System.Serializable]
public class Entity : MonoBehaviour
{
    private int level;
    private int health;
    private int attack;
    private int defense;
    public GameObject damageNumberPrefab;


    public void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.
        SetLevel(1);
        SetAttack(1);
        SetHealth(5);
        SetDefense(1);
        Debug.Log($"Entity {name} awake at {this.transform.position}");

    }

    public void Start(){
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();
            
        Debug.Log($"Entity {name} started at {this.transform.position}");

    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public void LevelUp()
    {
        this.level ++;  // plus 1
    }

public void SetAttack(int attack)
    {
        this.attack = attack;
    }

    public int GetAttack()
    {
        return attack;
    }

    public void AttackUp(int increase = 1){
        // default attack increase is 1
        // max of 8
        SetAttack(Math.Min(GetAttack()+increase,8));
    }

    public void SetDefense(int defense)
    {
        this.defense = defense;
    }

    public int GetDefense()
    {
        return defense;
    }

    public void DefenseUp(int increase = 1){
        // default defense increase is 1
        // max of 8
        SetDefense(Math.Min(GetDefense()+increase,8));        
    }


    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void HealthUp(int increase = 5){
        // default health increase is 5
        // max of 200
        SetHealth(Math.Min(GetHealth()+increase,200));        
    }    

    public void Attack(Entity other)
    {

        // if attack > other.defense then attack
        if (this.attack > other.defense)
        {
            Debug.Log($"{name} at {transform.position} attacks {other.name} at {other.transform.position} with Attack: {attack}");

            // call TakeDamage()
            other.TakeDamage(this);

            // draw attack line from enemy to other
            //drawLineToPlayer();
        }

    }

    public void TakeDamage(Entity other)
    {
        // other.attack > this.defense
        if (other.GetAttack() > this.defense)
        {
            // decrease health by actual damage.
            Debug.Log($"Other's attack > {name} defense.");
            int actualDamage = other.GetAttack() - this.defense;

            health -= (actualDamage);
            
            // Instantiate damage number prefab at this position
            GameObject damageNumberObj = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);

            if (damageNumberPrefab != null)
            {
                // Set the damage value on the damage number
                DamageNumber damageNumber = damageNumberObj.GetComponent<DamageNumber>();

                damageNumber.SetDamage(actualDamage);
                if (this.health <= 0)
                {
                    Die();
                }
            }
            else
            {
                Debug.Log("did not create damage number object.");
            }
        }

        // other.attack < this.defense
        else
        {
            Debug.Log($"{other.name}'s attack of {other.GetAttack()} < {name} defense of {this.defense}.");
        }
    }
    protected virtual void Die()
    {
        // Override this method in derived classes
        Debug.Log($"Entity {name} died!");
    }
    // public void drawLineToPlayer()
    // {
    //     // Set the positions for the LineRenderer (start and end points)
    //     lineRenderer.SetPosition(0, transform.position); // Start position: enemy's position
    //     lineRenderer.SetPosition(1, player.transform.position);    // End position: player's position

    //     // Enable the LineRenderer to make the line visible
    //     lineRenderer.enabled = true;

    //     // Start coroutine to disable LineRenderer after duration
    //     StartCoroutine(DisableLineRendererAfterDelay());
    // }
    // // Coroutine to disable LineRenderer after specified duration
    // private IEnumerator DisableLineRendererAfterDelay()
    // {
    //     yield return new WaitForSeconds(lineDuration);
    //     lineRenderer.enabled = false;
    // }

    public override string ToString()
    {
        string temp = $", Level: {level}";
        temp += $", Health: {health}";
        temp += $", Defense: {defense}";
        temp += $", Attack: {attack}";
        temp += $", Position: {transform.position}";
        return temp;
    }
}