using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public bool gameEnded;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject leaderboardCanvas;
    private Player player;

    public UnityEvent<string, int> submitScoreEvent;

    //[SerializeField] private TextMeshProUGUI inputScore;

    [SerializeField] private TextMeshProUGUI inputName;
    [SerializeField] private TextMeshProUGUI usernameText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameEnded = false;
        Time.timeScale = 0f;
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        leaderboardCanvas.SetActive(false);
    }

    public void loadGame()
    {
        // update player username
        if(inputName != null) {
            player.username = inputName.text;
            usernameText.text = player.username;
            
        }

        if (gameEnded == true)
        {
            player.health = 1000;
            gameEnded = false;
        }
        Debug.Log($"{player.username} playing game.");
        Time.timeScale = 1f;
        gameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        leaderboardCanvas.SetActive(false);

    }

    public void loadGameOver()
    {



        gameEnded = true;
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        leaderboardCanvas.SetActive(false);

    }

    public void loadSettings()
    {
        Time.timeScale = 0f;
        settingsCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        leaderboardCanvas.SetActive(false);

    }

    public void loadLeaderboard(){

        // update high score
        submitScoreEvent.Invoke(player.username, player.score);
        Debug.Log($"Loading leaderboard - Player: {player.username}, Score: {player.score}");
    
        leaderboardCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(false);

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
