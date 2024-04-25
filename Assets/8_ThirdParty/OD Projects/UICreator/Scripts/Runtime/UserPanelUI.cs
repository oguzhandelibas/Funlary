using TMPro;
using UnityEngine;

namespace Funlary.UIModule.Core
{
    public class UserPanelUI : View
    {
        [SerializeField] private TMP_InputField _tmpInputField;
        private string _userName;

        public bool HasUsername() => _userName.Length > 2;

        public void _SaveUsername(string username)
        {
            _tmpInputField.text = username;
            _userName = username;
        }
    }
}
