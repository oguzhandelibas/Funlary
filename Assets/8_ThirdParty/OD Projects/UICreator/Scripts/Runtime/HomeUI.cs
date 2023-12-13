using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary.UIModule
{
    public class HomeUI : View
    {
        [SerializeField] private GameObject posePlatform;

        public override void Show()
        {
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
            GameUIManager.Instance.Show<LeadboardUI>();
        }

        public void _SaveNameButton()
        {
            PlayfabManager.Instance.SubmitName();
        }
    }
}
