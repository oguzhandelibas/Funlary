using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public interface IStack
    {
        ColorType StackColorType { get; set; }
        bool CanCollectable { get; set; }
        bool SetAsStairStep { get; set; }
        void MoveTo(Transform parent, float height);
        void SetAsStep(Vector3 position);
        void DropStack();
        void SetColor(ColorType colorType, Material targetMaterial, float duration = 0.3f);
        void SetColor(ColorType colorType, Color targetColor, float duration = 0.3f);
        void SetAsCollectable();
    }
}
