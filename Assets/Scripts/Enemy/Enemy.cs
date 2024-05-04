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
    //private bool scoreable = true;
    public float enemySpeed { get; private set; }
    public float minEngagementDistance { get; private set; }
    public float maxEngagementDistance { get; private set; }
    public float waitTime { get; private set; }

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

        // Score credit as a function of level, defense, and attack
        int level = GetLevel();
        int defense = GetDefense(); // Assume GetDefense() returns the enemy's defense value
        int attack = GetAttack(); // Assume GetAttack() returns the enemy's attack value

        // Adjust scoreCredit based on level, defense, and attack
        // You can modify this formula based on your game's logic
        this.scoreCredit = level * (defense + attack);

        switch(GetLevel()) 
        {

        case 1: // easy - light gray
            SetHealth(10);
            SetSpriteColor(new Vector4(.88f, .88f, .88f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));  
            enemySpeed = 5f;
            minEngagementDistance = 5f;
            maxEngagementDistance = 20f;
            waitTime = .1f;     
            //SetAttack(10);        THESE ARE FOR DEBUGGING TEST CASE
            //SetDefense(10);
            break;

        case 2: // med - light blue
            SetHealth(16);            
            SetSpriteColor(new Vector4(.36f, .55f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));
            enemySpeed = 7f;
            minEngagementDistance = 7f;
            maxEngagementDistance = 20f;
            waitTime = 1.5f;
            break;
            
        case 3: // hard - green
            SetHealth(24); 
            SetSpriteColor(new Vector4(.4f, 1, .59f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));  
            enemySpeed = 9f;
            minEngagementDistance = 10f;
            maxEngagementDistance = 25f;
            waitTime = 1f;                     
            break;

        case 4: // super - yellow
            SetHealth(30); 
            SetSpriteColor(new Vector4(.91f, .91f, .31f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));
            enemySpeed = 11f;
            minEngagementDistance = 12f;
            maxEngagementDistance = 30f;
            waitTime = 0.8f;                       
            break;

        case 5: // mega - orange
            SetHealth(36); 
            SetSpriteColor(new Vector4(1, .83f, .27f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));
            enemySpeed = 13f;
            minEngagementDistance = 15f;
            maxEngagementDistance = 35f;
            waitTime = 0.6f;                       
            break;

        case 6: // ultra - red
            SetHealth(45); 
            SetSpriteColor(new Vector4(1, 0.27f, 0.34f, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));
            enemySpeed = 15f;
            minEngagementDistance = 17f;
            maxEngagementDistance = 40f;
            waitTime = 0.4f;                       
            break;

        case 7: // boss - purple
            SetHealth(60); 
            SetSpriteColor(new Vector4(.55f, .37f, 1, 1));
            SetAttackColor(Brighten(GetSpriteColor(), 0.5f));
            enemySpeed = 17f;
            minEngagementDistance = 20f;
            maxEngagementDistance = 45f;
            waitTime = 0.2f;                       
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
