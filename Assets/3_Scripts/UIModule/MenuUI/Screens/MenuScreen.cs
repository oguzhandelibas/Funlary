using System;
using Funlary.UIModule.Core;
using UnityEngine;

namespace Funlary.UIModule.Menu.Screens
{
    public class MenuScreen : View
    {
        private void Start()
        {
            
        }

        #region UI BUTTONS
        public void _OpenMyRoomScreen()
        {
            
        }
        public void _OpenUnitSelectionScreen()
        {
            MenuUIManager.Instance.Show<UnitSelectionScreen>();
        }
        public void _OpenStatusScreen()
        {
            MenuUIManager.Instance.Show<StatusScreen>();
        }
        public void _OpenProfileScreen()
        {
            MenuUIManager.Instance.Show<ProfileScreen>();
        }

        public void _OpenSettingsScreen()
        {
            MenuUIManager.Instance.Show<SettingsScreen>();
        }
        #endregion
        
    }
}
