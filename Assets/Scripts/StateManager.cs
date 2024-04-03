using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadGameOver()
    {
        gameEnded = true;
        SceneManager.LoadScene("GameOver");
    }

    public void loadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
