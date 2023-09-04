using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    public interface IStep
    {
        bool Used { get; set; }
        IStep NextStep { get; set; }
        ColorType StepColorType { get; set; }
        StepManager GetStepManager();
        Vector3 Position();
        int Index { get; set; }
        void InitializeStep(StepManager _stepManager, IStep _nextStep, Vector3 _localPos, Vector3 _localScale, int _index);
        void SetActiveness(bool gameObjectActiveness, bool wallObjectActiveness);
        void SetColor(Color color, ColorType stepColorType);
        ColorType GetColor();
    }
}
