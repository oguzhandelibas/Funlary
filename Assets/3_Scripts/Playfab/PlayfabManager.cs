using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Funlary;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : AbstractSingleton<PlayfabManager>
{
    [SerializeField] private UserProfileManager userProfileManager;
    [SerializeField] private NameData nameData;
    
    [SerializeField] private GameObject connectingText;
    [SerializeField] LeadboardManager leadboardManager;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] User ourPlayer;
    private string playfabId;
    
    [Header("INTERNET OBJECTS")] 
    [SerializeField] private GameObject leadboardButton;
    [SerializeField] private GameObject profileButton;
    
    private void Start()
    {
        connectingText.SetActive(true);
        leadboardButton.SetActive(false);
        profileButton.SetActive(false);
        
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
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnErrorLogin);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Succesful login/account create!");

        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoError);
    }

    private void OnGetAccountInfoError(PlayFabError obj)
    {
        Debug.LogError("GetAccountInfo error");
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult obj)
    {
        playfabId = obj.AccountInfo.PlayFabId;
        string username = "";
        PlayFabClientAPI.GetPlayerProfile( 
            new GetPlayerProfileRequest() {
                PlayFabId = playfabId,
                ProfileConstraints = new PlayerProfileViewConstraints() {
                    ShowDisplayName = true
                }
            },
            result => userProfileManager.SetUserProfile(result.PlayerProfile.DisplayName),
            error => Debug.LogError(error.GenerateErrorReport())
            
            );
    }

    #region ERROR DEBUG

    //----------------ERROR---------------------
    
    void OnErrorLogin(PlayFabError error)
    {
        Debug.Log("Error while logging in! " + error.GenerateErrorReport());
        Debug.Log(error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
    
    
    void OnErrorGetLeadboard(PlayFabError error)
    {
        Debug.Log("Error while Get Leadboard! " + error.GenerateErrorReport());
        Debug.Log(error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
    
    void OnErrorSubmitName(PlayFabError error)
    {
        Debug.Log("Error while Submit Name! " + error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
//----------------ERROR---------------------

    #endregion

    public void SendLeaderboard(int score)
    {
        
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new()
                    {
                        StatisticName = "UserTimes",
                        Value = score
                    }
                }
            },
            response => Debug.Log($"Succesfull leaderboard sent. Score {score}"),
            error => Debug.Log($"Error while Send Leadboard! {error.GenerateErrorReport()}")
        );
    }
    
    public void SubmitName(string username)
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = username,
        };
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnErrorSubmitName);
    }
    
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name to : " + result.DisplayName);
        connectingText.SetActive(false);
        SendLeaderboard(PlayerPrefs.GetInt("BestTime", 0));
        GetLeaderboard();
    }
    
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "UserTimes",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnErrorGetLeadboard);
    }
    
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        int index = 0;
        for (var i = result.Leaderboard.Count - 1; i >= 0; i--)
        {
            if (result.Leaderboard[i].StatValue > 0)
            {
                //Debug.Log($"Index: {index}, Name: {result.Leaderboard[i].DisplayName}");
                leadboardManager.users[index].SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, index, true);
                index++;
            }
            
            //Debug.Log($"ID_1: {playfabId}, ID_2: {result.Leaderboard[i].PlayFabId}");
            if (playfabId == result.Leaderboard[i].PlayFabId)
            {
                ourPlayer.SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);
                inputField.text = result.Leaderboard[i].DisplayName;
            }
        }
        
        leadboardButton.SetActive(true);
        profileButton.SetActive(true);
    }
}