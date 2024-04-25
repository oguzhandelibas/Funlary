using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using TMPro;
using UnityEngine;

namespace Funlary.UIModule
{
    public class HomeUI : View
    {
        [SerializeField] TMP_InputField inputField;
        [SerializeField] private GameObject posePlatform;
        
        public override void Show()
        {
            CameraController.Instance.VirtualCameraActiveness(false);
            posePlatform.SetActive(true);
            base.Show();
        }

        public override void Hide()
        {
            posePlatform.SetActive(false);
            base.Hide();
        }

        public void _PlayButton()
        {
            GameUIManager.Instance.Show<GameUI>();
            GameManager.Instance.PlayGame();
        }

        public void _OpenLeadboard()
        {
            PlayfabManager.Instance.GetLeaderboard();
            GameUIManager.Instance.Show<LeadboardUI>();
        }

        public void _SaveNameButton()
        {
            PlayfabManager.Instance.SubmitName(inputField.text);
        }
        
    }
}
