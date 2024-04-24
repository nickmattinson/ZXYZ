using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputScore;
    [SerializeField] TextMeshProUGUI inputName;
    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore(){
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }

}