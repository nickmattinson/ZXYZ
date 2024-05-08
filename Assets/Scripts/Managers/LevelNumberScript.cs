using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelNumberScript : MonoBehaviour
{
    TextMeshProUGUI levelNumber;
    int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        levelNumber = GetComponent<TextMeshProUGUI>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        levelNumber.text = "Level " + sceneIndex;
    }
}
