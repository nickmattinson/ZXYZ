using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null; // singleton
    GameObject levelSign, gameOverText, youWinText;
    int sceneIndex, levelPassed;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance = this)
        {
            Destroy(gameObject);
        }

        levelSign = GameObject.Find("LevelNumber");
        gameOverText = GameObject.Find("GameOverText");
        youWinText = GameObject.Find("YouWinText");
        gameOverText.gameObject.SetActive(false);
        youWinText.gameObject.SetActive(false);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");

    }

    public void youWin(){
        if(sceneIndex == 3){
            Invoke("loadMainMenu", 1f);
        } else {
            if(levelPassed < sceneIndex)
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);
            levelSign.gameObject.SetActive(false);
            youWinText.gameObject.SetActive(true);
            Invoke("loadNextLevel", 1f);
        }
    }

    public void youLose(){
        levelSign.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        Invoke("loadMainMenu", 1f);
    }


    public void loadNextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void loadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);

    }

}
