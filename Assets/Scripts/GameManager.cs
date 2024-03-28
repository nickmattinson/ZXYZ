using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool gameEnded = false;
    protected GameObject backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Instantiate(player, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            loadSettings();
        }
    }

    public void loadGame(){
        SceneManager.LoadScene("Main");
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void loadGameOver(){
        gameEnded = true;
        SceneManager.LoadScene("GameOver");
    }

    public void loadSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void quitGame(){
        Application.Quit();
    }
}
