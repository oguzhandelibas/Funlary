using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Funlary.UIModule.Menu.Screens;

namespace Funlary.UIModule.Menu.Unit
{
    public class UnitBlock : MonoBehaviour
    {
        [SerializeField] private Button unitButton;
        [SerializeField] private TextMeshProUGUI UnitName;
        [SerializeField] private TextMeshProUGUI UnitExplanation;
        private int UnitIndex;
        public void SetUnit(string unitName, string unitExplanation, int unitIndex, bool active = false)
        {
            this.UnitIndex = unitIndex;
            unitButton.interactable = active;
            UnitName.text = unitName;
            UnitExplanation.text = unitExplanation;
        }
        
        public void _SelectUnit()
        {
            MenuUIManager.Instance.UnitIndex = UnitIndex;
            MenuUIManager.Instance.Show<GameSelectionScreen>();
        }
    }
}
