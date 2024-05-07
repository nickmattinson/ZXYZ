using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StateManager stateManager;
    [SerializeField] GameObject player;
    protected GameObject backgroundMusic;
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        Application.targetFrameRate = 60;
        player = Instantiate(player, Vector2.zero, Quaternion.identity);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateManager.loadSettings();
        }
    }
}
