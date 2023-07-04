using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    private string publicLeaderBoardKey = "a96805bf4dc2ca6cff15108f27b4d232c98e31bbf7bbd8ccff5bb9f1f0c75e61";

    private void Start() {
        GetLeaderboard();
    }
    public void GetLeaderboard() {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i) {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score) {
        if (username.Length > 5) {
            username = username.Substring(0,5);
        }

        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, ((msg) => {
            LeaderboardCreator.ResetPlayer();
            Invoke("GetLeaderboard", 10f);
        }));
        Debug.Log("Got");
    }
}
