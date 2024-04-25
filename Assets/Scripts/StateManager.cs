using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    [SerializeField] private TextMeshProUGUI playerprefText;

    // Start is called before the first frame update
    void Start()
    {
        // player reference
        player = FindObjectOfType<Player>();

        // look for current player pref
        //if(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerUserName")))
        Debug.Log($"Starting StateManager before - PlayerPrefs Username: {PlayerPrefs.GetString("PlayerUserName")}");
        if(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerUserName"))){
            player.username = PlayerPrefs.GetString("PlayerUserName");
            playerprefText.text = PlayerPrefs.GetString("PlayerUserName");
            //usernameText.text = player.username;
        }       
        Debug.Log($"Starting StateManager after - PlayerPrefs Username: {PlayerPrefs.GetString("PlayerUserName")}");

        // setup the canvas
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
        // Make sure inputName is assigned correctly in the Unity Editor
        if (inputName != null && !string.IsNullOrEmpty(inputName.text)) {
            player.username = playerprefText.text;
            PlayerPrefs.SetString("PlayerUserName", player.username);
            usernameText.text = player.username; // Update UI
        } else {
            player.username = "Player"; // Default username
            PlayerPrefs.SetString("PlayerUserName", player.username);
            usernameText.text = player.username; // Update UI
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
