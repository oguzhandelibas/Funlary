using System;
using UnityEngine;
using Funlary.UIModule.Core;
using Funlary.UIModule.Menu.Unit;

namespace Funlary.UIModule.Menu.Screens
{
    public class GameSelectionScreen : View
    {
        [SerializeField] private GameSelectionPanelData GameSelectionPanelData;

        private void OnEnable()
        {
            GameObject obj = GameSelectionPanelData.GameSelectionPanels[MenuUIManager.Instance.UnitIndex];
            Instantiate(obj,transform);
        }

        public void _SelectGame()
        {
            
        }
    }
}