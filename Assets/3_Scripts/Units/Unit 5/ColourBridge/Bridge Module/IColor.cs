using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary
{
    public interface IColor
    {
        bool CheckColor(ColorType targetColor);
        Color GetColor();
    }
}
