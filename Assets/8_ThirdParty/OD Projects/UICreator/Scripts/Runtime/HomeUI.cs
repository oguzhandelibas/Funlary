using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary.UIModule
{
    public class HomeUI : View
    {
        public void _PlayButton()
        {
            GameUIManager.Instance.Show<GameUI>();
            GameManager.Instance.SetLevel();
        }
    }
}
