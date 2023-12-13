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
    [SerializeField] User ourPlayer;
    private string playfabId;
    
    [Header("INTERNET OBJECTS")] 
    [SerializeField] private GameObject leadboardButton;
    [SerializeField] private GameObject profileButton;
    
    
    

    private void Start()
    {
        //GameManager.Instance.LeadboradButtonActiveness(false);
        //introController = GetComponent<IntroController>();
        leadboardButton.SetActive(false);
        profileButton.SetActive(false);
        
        PlayerPrefs.SetInt("BestTime", 2);
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
        
        SendLeaderboard(PlayerPrefs.GetInt("BestTime", 0));
        
        
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
        GetLeaderboard();
    }

    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputField.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnErrorSubmitName);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        //nameInputField.SetActive(false); ----------
        //PlayerController.Instance.gameActive = true;
    }

    
    //----------------ERROR---------------------
    void OnErrorLogin(PlayFabError error)
    {
        Debug.Log("Error while logging in! " + error.GenerateErrorReport());
        Debug.Log(error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
    
    void OnErrorSendLeadboard(PlayFabError error)
    {
        Debug.Log("Error while Send Leadboard! " + error.GenerateErrorReport());
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
        Debug.Log(error.GenerateErrorReport());
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
//----------------ERROR---------------------

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
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnErrorSendLeadboard);
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
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnErrorGetLeadboard);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        for (var i = 0; i < result.Leaderboard.Count; i++)
        {
            leadboardManager.users[i].SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);
            if (playfabId == result.Leaderboard[i].PlayFabId)
            {
                ourPlayer.SetInfo(result.Leaderboard[i].DisplayName + " (YOU)", result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);
                inputField.text = result.Leaderboard[i].DisplayName;
            }
        }
        
        leadboardButton.SetActive(true);
        profileButton.SetActive(true);
    }
}