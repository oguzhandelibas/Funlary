using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public interface IStack
    {
        ColorType StackColorType { get; set; }
        bool CanCollectable { get; set; }
        bool SetAsStairStep { get; set; }
        void Collect(Opponent opponent, Transform parent, float height);
        void SetAsStep(Vector3 position);
        void DropStack(bool destroyAfter = false);
        void SetColor(ColorType targetColorType, ColorData colorData, float duration = 0.3f);
    }
}
