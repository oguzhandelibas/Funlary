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
        //GameManager.Instance.LeadboradButtonActiveness(false);
        //introController = GetComponent<IntroController>();
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
        
        SendLeaderboard(PlayerPrefs.GetInt("BestTime", 0));

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
        connectingText.SetActive(false);
        GetLeaderboard();
        //SubmitName();
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
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
        //GameManager.Instance.SetInternetObjectsActiveness(false);
        //introController.StopAnimation();
    }
//----------------ERROR---------------------

    #endregion

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {
                    StatisticName = "UserScores",
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
            StatisticName = "UserScores",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnErrorGetLeadboard);
    }
    
    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = inputField.text,
        };
        
        if (request.DisplayName.Length < 2)
        {
            request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameData.Names[Random.Range(0, nameData.Names.Length)],
            };
        }
        
        inputField.text = request.DisplayName;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnErrorSubmitName);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        int index = 0;
        for (var i = result.Leaderboard.Count - 1; i >= 0; i--)
        {
            if (result.Leaderboard[i].StatValue > 0)
            {
                leadboardManager.users[index].SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, index, true);
                index++;
            }
            
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