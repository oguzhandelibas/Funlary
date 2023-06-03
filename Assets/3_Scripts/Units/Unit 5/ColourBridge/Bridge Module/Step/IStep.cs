using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public interface IStep
    {
        void InitializeStep(Vector3 _localPos, Vector3 _localScale);
        void SetActiveness(bool gameObjectActiveness, bool triggerActiveness);
        void SetColor(Color color);
    }
}
