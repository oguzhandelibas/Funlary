using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Funlary.UIModule.Menu.Unit
{
    public class UnitBlock : MonoBehaviour
    {
        [SerializeField] private Button unitButton;
        [SerializeField] private TextMeshProUGUI UnitName;
        [SerializeField] private TextMeshProUGUI UnitExplanation;

        public void SetUnit(string unitName, string unitExplanation, bool active = false)
        {
            unitButton.interactable = active;
            UnitName.text = unitName;
            UnitExplanation.text = unitExplanation;
        }
    }
}
