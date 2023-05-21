using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Funlary.UIModule.Unit
{
    public class UnitBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI UnitName;
        [SerializeField] private TextMeshProUGUI UnitExplanation;

        public void SetUnit(string unitName, string unitExplanation)
        {
            UnitName.text = unitName;
            UnitExplanation.text = unitExplanation;
        }
    }
}
