using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] GameObject easyTutorialEnemy;
    // [SerializeField] GameObject mediumTutorialEnemy;
    // [SerializeField] GameObject medium2TutorialEnemy;
    public StateManager stateManager;
    [SerializeField] GameObject player;
    protected GameObject backgroundMusic;
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        Application.targetFrameRate = 60;
        // Vector2 easyTutorialEnemyv2 = new Vector2();
        // Vector2 mediumTutorialEnemyv2 = new Vector2();
        // Vector2 medium2TutorialEnemyv2 = new Vector2();
        // easyTutorialEnemyv2.x = 0f;
        // easyTutorialEnemyv2.y = 74f;
        // mediumTutorialEnemyv2.x = -5f;
        // mediumTutorialEnemyv2.y = 42f;
        // medium2TutorialEnemyv2.x = 5f;
        // medium2TutorialEnemyv2.y = 42f;
        // easyTutorialEnemy = Instantiate(easyTutorialEnemy, easyTutorialEnemyv2, Quaternion.identity);
        // mediumTutorialEnemy = Instantiate(mediumTutorialEnemy, mediumTutorialEnemyv2, Quaternion.identity);
        // medium2TutorialEnemy = Instantiate(medium2TutorialEnemy, medium2TutorialEnemyv2, Quaternion.identity);
        player = Instantiate(player, Vector2.zero, Quaternion.identity);

        // //easyTutorialEnemy attributes
        // easyTutorialEnemy.GetComponent<Enemy>().SetRespawn(false);
        // easyTutorialEnemy.GetComponent<Enemy>().SetAttack(3);
        // easyTutorialEnemy.GetComponent<Enemy>().SetHealth(5);
        // easyTutorialEnemy.GetComponent<Enemy>().SetDefense(2);
        // easyTutorialEnemy.GetComponent<Enemy>().SetSpriteColor(new Vector4(0, 1, 0, 1));
        // easyTutorialEnemy.GetComponent<Enemy>().SetAttackColor(new Vector4(0, 1, 0, 1));            
        // easyTutorialEnemy.GetComponent<Enemy>().SetLevel(1);
        // //easyTutorialEnemy.GetComponent<Enemy>().SetCapability();

        // //medium TutorialEnemy attributes
        // mediumTutorialEnemy.GetComponent<Enemy>().SetRespawn(false);
        // mediumTutorialEnemy.GetComponent<Enemy>().SetAttack(4);
        // mediumTutorialEnemy.GetComponent<Enemy>().SetHealth(10);
        // mediumTutorialEnemy.GetComponent<Enemy>().SetDefense(3);
        // mediumTutorialEnemy.GetComponent<Enemy>().SetSpriteColor(new Vector4(0.83f, 0.68f, 0.39f, 1));
        // mediumTutorialEnemy.GetComponent<Enemy>().SetAttackColor(new Vector4(0.83f, 0.68f, 0.39f, 1));        
        // mediumTutorialEnemy.GetComponent<Enemy>().SetLevel(2);
        // //mediumTutorialEnemy.GetComponent<Enemy>().SetCapability();

        // //medium2 TutorialEnemy attributes
        // medium2TutorialEnemy.GetComponent<Enemy>().SetRespawn(false);
        // medium2TutorialEnemy.GetComponent<Enemy>().SetAttack(4);
        // medium2TutorialEnemy.GetComponent<Enemy>().SetHealth(10);
        // medium2TutorialEnemy.GetComponent<Enemy>().SetDefense(3);
        // medium2TutorialEnemy.GetComponent<Enemy>().SetSpriteColor(new Vector4(0.83f, 0.68f, 0.39f, 1));
        // medium2TutorialEnemy.GetComponent<Enemy>().SetAttackColor(new Vector4(0.83f, 0.68f, 0.39f, 1));        
        // medium2TutorialEnemy.GetComponent<Enemy>().SetLevel(2);
        // //medium2TutorialEnemy.GetComponent<Enemy>().SetCapability();

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateManager.loadSettings();
        }
    }
}
