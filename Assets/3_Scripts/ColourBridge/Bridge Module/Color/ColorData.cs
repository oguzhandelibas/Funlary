using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "BridgeModule/ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        [SerializedDictionary("Color Type", "Material")]
        public SerializedDictionary<ColorType, Material> ColorType;

        public ColorType GetRandomColorType(List<ColorType> colorTypes)
        {
            List<ColorType> keys = new List<ColorType>(ColorType.Keys);
            int it = (int)colorTypes[Random.Range(0, colorTypes.Count)];
            return keys[it - 1];
        }
    }
}
