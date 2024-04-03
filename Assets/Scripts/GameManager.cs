using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject easyTutorialEnemy;
    [SerializeField] GameObject mediumTutorialEnemy;
    [SerializeField] GameObject medium2TutorialEnemy;
    public StateManager stateManager;
    [SerializeField] GameObject player;
    protected GameObject backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        Application.targetFrameRate = 60;
        Vector2 easyTutorialEnemyv2 = new Vector2();
        Vector2 mediumTutorialEnemyv2 = new Vector2();
        Vector2 medium2TutorialEnemyv2 = new Vector2();
        easyTutorialEnemyv2.x = 0f;
        easyTutorialEnemyv2.y = 74f;
        mediumTutorialEnemyv2.x = -5f;
        mediumTutorialEnemyv2.y = 42f;
        medium2TutorialEnemyv2.x = 5f;
        medium2TutorialEnemyv2.y = 42f;
        Instantiate(easyTutorialEnemy, easyTutorialEnemyv2, Quaternion.identity);
        Instantiate(mediumTutorialEnemy, mediumTutorialEnemyv2, Quaternion.identity);
        Instantiate(medium2TutorialEnemy, medium2TutorialEnemyv2, Quaternion.identity);
        Instantiate(player, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateManager.loadSettings();
        }
    }


}
