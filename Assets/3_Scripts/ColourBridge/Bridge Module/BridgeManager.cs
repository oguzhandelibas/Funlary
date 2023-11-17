using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using Unity.VisualScripting;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class BridgeManager : AbstractSingleton<BridgeManager>
    {
        [SerializeField] private List<Bridge> bridges;
        [SerializeField] private List<ColorType> colorTypes;
        [SerializeField] private OpponentManager opponentManager;

        private void Start()
        {
            opponentManager.SetColorTypes(colorTypes);
        }

        public List<ColorType> AddBridge(Bridge bridgeTemp)
        {
            bridges.Add(bridgeTemp);
            return colorTypes;
        }

        public List<ColorType> GetColorTypes()
        {
            return colorTypes;
        }
    }
}
