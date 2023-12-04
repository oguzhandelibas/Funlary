using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary.UIModule.Core
{
    public class LevelCompletedUI : View
    {
        public void _NextLevel()
        {
            GameManager.Instance.LevelIndex++;
        }
    }
}
