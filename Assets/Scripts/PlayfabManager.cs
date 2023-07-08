using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rowPrefab;
    public Transform rowsParent;

    [SerializeField] TextMeshProUGUI inputScore;
    [SerializeField] TMP_InputField inputUser;
    string username; 
    int score;

    public void Login()
    {
        username = inputUser.text;
        if (username.Length > 8)
        {
            username = username.Substring(0, 7);
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = username.ToString(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        // Handle successful login
        // Set the display name
        var displayNameRequest = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(displayNameRequest, OnSuccessUpdateDisplayName, OnError);
    }

    private bool displayNameUpdated = false;

    private void OnSuccessUpdateDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Successfully updated display name.");
        displayNameUpdated = true;
    }

    public void onViewClick()
    {
        Login();
        StartCoroutine(GetLeaderboardWithDelay());
    }

    IEnumerator GetLeaderboardWithDelay()
    {
        yield return new WaitUntil(() => displayNameUpdated);
        GetLeaderboard();
    }

    public void onPostClick() {
        Login();
        Invoke("SendLeaderboard", 2f);
    }

    void OnError (PlayFabError error) {
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard() {
        score = int.Parse(string.Join("", inputScore.text.ToCharArray().Where(Char.IsDigit)));
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Flips",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    
    void OnLeaderBoardGet(GetLeaderboardResult result) {

        foreach(Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard) {        
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }

    }

    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Flips",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful update.");
    }
}

