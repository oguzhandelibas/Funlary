using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "BridgeModule/ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        public Dictionary<ColorType, Color> colorType;
    }
}
