using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded;

    protected GameObject backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");     //Or any scene I want to open when death occurs... i.e. GameOver Scene

            }
        }

    }

    public void gameOver()
    {

    }

    public void loadGame(){
        SceneManager.LoadScene("Main");
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void loadGameOver(){
        SceneManager.LoadScene("GameOver");
    }

    public void loadSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void quitGame(){
        Application.Quit();
    }
}
