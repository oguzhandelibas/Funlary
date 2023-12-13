using System.IO;
using System.Collections;
using System.Collections.Generic;
using Funlary;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : AbstractSingleton<PlayfabManager>
{
    [SerializeField] LeadboardManager leadboardManager;
    [SerializeField] TMP_InputField inputField;
    //IntroController introController;
    [SerializeField] User ourPlayer;
    private string playfabId;
    

    private void Start()
    {
        //GameManager.Instance.LeadboradButtonActiveness(false);
        //introController = GetComponent<IntroController>();
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);

    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Succesful login/account create!");
        
        SendLeaderboard(PlayerPrefs.GetInt("BestScore"));
        GetLeaderboard();

        string name = null;

        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
        {
            //PlayerController.Instance.gameActive = false;
            //nameInputField.SetActive(true); ---------
        }
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoError);
        
        //introController.StopAnimation();

    }

    private void OnGetAccountInfoError(PlayFabError obj)
    {
        Debug.LogError("GetAccountInfo error");
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult obj)
    {
        //GameManager.Instance.SetInternetObjectsActiveness(true);
        //GameManager.Instance.LeadboradButtonActiveness(true);
        playfabId = obj.AccountInfo.PlayFabId;
        Debug.Log("PlayFab ID: " + playfabId); 
    }

    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputField.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        //nameInputField.SetActive(false); ----------
        //PlayerController.Instance.gameActive = true;
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {
                    StatisticName = "UserTime",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "UserTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        for (var i = 0; i < result.Leaderboard.Count; i++)
        {
            leadboardManager.users[i].SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);

            if (result.Leaderboard[i].PlayFabId == playfabId)
            {
                ourPlayer.SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);
            }
                
        }
    }
}