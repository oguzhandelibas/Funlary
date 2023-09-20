using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary
{
    public class ColorManager : AbstractSingleton<ColorManager>
    {
        [SerializeField] private ColorData colorData;

        public Color GetColor(ColorType colorType)
        {
            return colorData.ColorType[colorType].color;
        }
    }
}
