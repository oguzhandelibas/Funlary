using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using Unity.VisualScripting;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class BridgeManager : MonoBehaviour
    {
        [SerializeField] private List<ColorType> colorTypes;
        [SerializeField] private OpponentManager opponentManager;

        private void OnEnable()
        {
            opponentManager.SetColorTypes(colorTypes);
        }

        public List<ColorType> GetColorTypes()
        {
            return colorTypes;
        }
    }
}
