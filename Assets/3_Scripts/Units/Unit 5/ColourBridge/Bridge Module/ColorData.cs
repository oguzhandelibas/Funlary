using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "BridgeModule/ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        [SerializedDictionary("Color Type", "Color")]
        public SerializedDictionary<ColorType, Color> ColorType;
    }
}
