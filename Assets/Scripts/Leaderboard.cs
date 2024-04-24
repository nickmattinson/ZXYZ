using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
public class Leaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;

    [SerializeField] private Player player;
    
    private string publicLeaderboardKey = "93f3cc82eca0ca333cde25f1d919a35511cf3653ec219ac03c9dfdc903008ad8";

    private void Start(){
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        // get player score
        player = FindObjectOfType<Player>();
        score = player.score;

        // set score to player's score


        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            // limit username length...

            // check for and remove bad words

            // update leader board
            GetLeaderboard();
        }));
    }

}