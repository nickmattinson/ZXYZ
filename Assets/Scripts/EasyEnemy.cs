// EasyEnemy.cs
//using System.Drawing;

using UnityEngine;


public class EasyEnemy : Enemy
{

    //private Player player;


    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        SetLevel(1);
        SetAttack(2);
        SetHealth(5);
        SetDefense(3);
        SetSpriteColor(new Color(0.0f, 1.0f, 0.0f)); // green
        SetAttackColor(new Color(0.0f, 0.39f, 0.0f)); // dark green


        Debug.Log($"[{this.name}] {this} ____ AWAKE.");

    }

    
    protected override void Die()
    {
        // Call the base class Die method to execute its logic
        base.Die();

    }
    public new void Start(){
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.
        base.Start();

        Debug.Log($"[{this.name}] {this} ____ STARTED.");

    }
}