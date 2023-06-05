using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public interface IStep
    {
        void InitializeStep(StepManager _stepManager, Vector3 _localPos, Vector3 _localScale, int _index);
        void SetActiveness(bool gameObjectActiveness, bool wallObjectActiveness);
        void SetColor(Color color);
    }
}
