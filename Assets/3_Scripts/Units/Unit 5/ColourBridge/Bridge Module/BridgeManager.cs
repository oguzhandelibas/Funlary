using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public class BridgeManager : MonoBehaviour
    {
        [SerializeField] private Bridge[] bridges;
        [SerializeField] private List<ColorType> colorTypes;
        [SerializeField] private OpponentManager opponentManager;

        private void Start()
        {
            foreach (var item in bridges)
            {
                item.SetStackManagerColorTypes(colorTypes);
            }
            opponentManager.SetColorTypes(colorTypes);
        }
    }
}
