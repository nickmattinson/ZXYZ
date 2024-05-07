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
    [SerializeField] private Player player;

    public UnityEvent<string, int> submitScoreEvent;

    //[SerializeField] private TextMeshProUGUI scoreValue;

    public TextMeshProUGUI output;
    public TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI usernameIngame; // display


    // Start is called before the first frame update
    void Start()
    {
        // Check if PlayerPrefs has a stored username
        player = FindObjectOfType<Player>();
        string storedUsername = PlayerPrefs.GetString("PlayerUserName");
        if (!string.IsNullOrEmpty(storedUsername))
        {
            // Set usernameInput and usernameIngame
            usernameInput.text = storedUsername;
            usernameIngame.text = storedUsername;
            output.text = storedUsername;
        }

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

        //Check if input is provided in usernameInput
        if (!string.IsNullOrEmpty(usernameInput.text))
        {
            // Set player.username using usernameInput
            player.username = usernameInput.text;
            PlayerPrefs.SetString("PlayerUserName", player.username);
        }
        else
        {
            // Set a default username
            player.username = "Player";
            PlayerPrefs.SetString("PlayerUserName", player.username);
        }

        // Update usernameIngame
        usernameIngame.text = player.username;

        // Debug logs to check values
        Debug.Log($"Player Username: {player.username}  ___USERNAME");
        //Debug.Log("Player Username saved in PlayerPrefs: " + PlayerPrefs.GetString("PlayerUserName"));


        // Rest of your code...
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

public void loadLeaderboard()
{
    //Check if input is provided in usernameInput
    if (!string.IsNullOrEmpty(usernameInput.text))
    {
        // Set player.username using usernameInput
        player.username = usernameInput.text;
        PlayerPrefs.SetString("PlayerUserName", player.username);
    }
    else
    {
        // Set a default username
        player.username = "Player";
        PlayerPrefs.SetString("PlayerUserName", player.username);
    }

    if (!string.IsNullOrEmpty(player.username))
    {
        // Update high score
        submitScoreEvent.Invoke(player.username, player.score);
        Debug.Log($"Loading leaderboard - Player: {player.username}, Score: {player.score}");

        leaderboardCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
    }
    else
    {
        Debug.LogError("Player username is null or empty when loading leaderboard!");
    }
}

    public void quitGame()
    {
        Application.Quit();
    }
}
