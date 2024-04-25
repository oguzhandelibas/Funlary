using Funlary.UIModule;
using Funlary.UIModule.Game;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Funlary
{
    public class UserProfileManager : MonoBehaviour
    {
        [SerializeField] private NameData nameData;
        [SerializeField] private GameObject userPanelUI;
        [SerializeField] private TMP_InputField _tmpInputField;
        [SerializeField] private string _username;
        private int _userScore;

        public void SetUserProfile(string username)
        {
            _username = username;
            
            if (_username.Length < 3)
            {
                _username = nameData.Names[Random.Range(0, nameData.Names.Length)];
                SetUserPanelActiveness(true);
            }
            else
            {
                SetUserPanelActiveness(false);
            }
            
            _tmpInputField.text = _username;
            PlayerPrefs.SetString("Username", username);
            PlayfabManager.Instance.SubmitName(username);
            
        }

        public void SetUserPanelActiveness(bool activeness)
        {
            userPanelUI.SetActive(activeness);
            if (!activeness)
            {
                GameUIManager.Instance.Show<HomeUI>();
            }
        }

        public bool SetUsername(string username)
        {
            if (username != null && _username != username && username.Length > 3)
            {
                SetUserPanelActiveness(false);
                return true;
            }
            
            SetUserPanelActiveness(true);
            return false;
        }
    }
}
