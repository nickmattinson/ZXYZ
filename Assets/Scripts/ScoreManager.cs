using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    public UnityEvent<string, int> submitScoreEvent;

    //[SerializeField] private Player player;

    public void SubmitScore(){

        // use player's score
        //player = FindObjectOfType<Player>();

        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }

}