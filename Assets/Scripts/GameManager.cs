using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StateManager stateManager;
    [SerializeField] GameObject player;
    protected GameObject backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        Application.targetFrameRate = 60;
        Instantiate(player, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            stateManager.loadSettings();
        }
    }

    
}
