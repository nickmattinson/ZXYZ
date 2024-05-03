using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    //private Player player;
    //private float lineDuration = 0.1f;
    [SerializeField] List<GameObject> powerupList;
    [SerializeField] int chanceForPowerUp;
    private Vector3 spawnPosition;
    private Vector3 deathPosition;
    GameObject enemyRef;
    public int scoreCredit = 1;
    private bool scoreable = true;

    public new void Awake(){
        // used for initial setup that
        // doesn't rely on other objects
        // or components being initialized.

        // get rid of the Clone reference    
        this.name = this.name.Replace("(Clone)","").Trim();

        // set enemy capability
        SetCapability();
        //Debug.Log($"[{this.name}] {this} ____ AWAKE.");
    }

    new void Start()
    {
        // used for initial setup that
        // does rely on other objects
        // or components being initialized.

        // Get the Player component attached to the GameObject
        Player player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player reference not found!_______ENEMY_START");
        }

        // set spawn position
        SetSpawnPosition(this.transform.position);

        Debug.Log($"[{this.name}] {this} ____ STARTED.");
    }

    public void SetCapability(){
        // player reference
        Player player = FindObjectOfType<Player>();

        // score credit as function of level
        this.scoreCredit = GetLevel();

        switch(GetLevel()) 
        {

        case 1: // easy - light gray
            //SetAttack(2);
            SetHealth(10);
            //SetDefense(3);
            SetSpriteColor(new Vector4(.88f, .88f, .88f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));       
            break;

        case 2: // med - light blue
            //SetAttack(3);
            //SetDefense(4);
            SetHealth(16);            
            SetSpriteColor(new Vector4(.36f, .55f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f)); 
            break;
            
        case 3: // hard - green
            //SetAttack(5);  // at least
            //SetDefense(4); // at least
            SetHealth(24); 
            SetSpriteColor(new Vector4(.4f, 1, .59f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 4: // super - yellow
            //SetAttack(7);  // at least
            //SetDefense(5); // at least
            SetHealth(30); 
            SetSpriteColor(new Vector4(.91f, .91f, .31f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 5: // mega - orange
            //SetAttack(9);  // at least
            //SetDefense(7); // at least
            SetHealth(36); 
            SetSpriteColor(new Vector4(1, .83f, .27f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 6: // ultra - red
            //SetAttack(13);  // at least
            //SetDefense(9); // at least
            SetHealth(45); 
            SetSpriteColor(new Vector4(1, 0.27f, 0.34f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        case 7: // boss - purple
            //SetAttack(20);  // at least
            //SetDefense(15); // at least
            SetHealth(60); 
            SetSpriteColor(new Vector4(.55f, .37f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));                       
            break;

        default:
            // Default 
            break;
        }
    }


    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }


    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }




    protected override void Die()
    {
        // Get player reference
        Player player = FindObjectOfType<Player>();

        // update player's score based on enemy score credit
        player.score += this.scoreCredit;
        player.SetLevel(Mathf.Max(player.score/5,1));

        // step 1 - set death position
        deathPosition = transform.position;
        Debug.Log($"[{name}-{this.GetLevel()}] killed by [{player.name}]. ____KILL");

        // step 2 - drop a random buf at death position randomly
        int rn = Random.Range(0, 100);
        //Debug.Log($"Random number: {randomNumber}");
        if (rn >= (100-chanceForPowerUp))
        {
            GameObject currentPowerupPrefab = powerupList[Random.Range(0, powerupList.Count)];
            GameObject powerupInstance = Instantiate(currentPowerupPrefab, deathPosition, Quaternion.identity);
            //Debug.Log($"Power up {powerupInstance} created.");
        }

        // step 3 - destroy object
        Destroy(gameObject);
    }

    public override string ToString()
    {
        string temp = $"{base.ToString()}";
        temp += $", Spawnpoint: {this.GetSpawnPosition()}";
        return temp;
    }
}
