using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public bool gameEnded = false;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {
        mainMenuCanvas.SetActive(false);
    }

    public void loadGameOver()
    {
        gameEnded = true;
        gameOverCanvas.SetActive(true);
    }

    public void loadSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
