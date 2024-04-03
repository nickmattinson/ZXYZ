using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public bool gameEnded;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject gameOverCanvas;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameEnded = false;
        Time.timeScale = 0f;
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void loadGame()
    {
        if(gameEnded == true) {
            player.health = 20;
            gameEnded = false;
        }
        Debug.Log("playing game");
        Time.timeScale = 1f;
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void loadGameOver()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }

    public void loadSettings()
    {
        Time.timeScale = 0f;
        settingsCanvas.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
