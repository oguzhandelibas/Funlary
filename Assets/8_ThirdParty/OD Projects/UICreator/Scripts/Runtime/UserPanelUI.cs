using TMPro;
using UnityEngine;

namespace Funlary.UIModule.Core
{
    public class UserPanelUI : View
    {
        [SerializeField] private TMP_InputField _tmpInputField;
        private string _userName;

        public void _SaveUserName() => _userName = _tmpInputField.text;
    }
}
