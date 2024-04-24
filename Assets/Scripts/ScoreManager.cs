using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    public UnityEvent<string, int> submitScoreEvent;

    [SerializeField] private Player player;

    public void SubmitScore(){

        // use player's score
        player = FindObjectOfType<Player>();

        // update the input for score to player's score
        inputScore.text = $"{player.score}";

        submitScoreEvent.Invoke(inputName.text, player.score);
    }

}