// EasyEnemy.cs
//using System.Drawing;

using UnityEngine;


public class EasyEnemyTwo : Enemy
{


    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        SetLevel(1);
        SetAttack(5);
        SetHealth(12);
        SetDefense(2);
        SetSpriteColor(new Color(0.0f, 0.0f, 1.0f)); // blue
        SetAttackColor(new Color(0.5f, 0.0f, 0.5f)); // dark green


        Debug.Log($"[{this.name}] {this} ____ AWAKE.");

    }

    public new void Start(){
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        Debug.Log($"[{this.name}] {this} ____ STARTED.");

    }
}