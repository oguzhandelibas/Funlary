using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public interface IStack
    {
        bool CanCollectable { get; set; }
        bool SetAsStairStep { get; set; }
        void MoveTo(Transform parent, float height);
        void SetAsStep(Vector3 position);
        void DropStack();
        void SetStackColor(Color targetColor, float duration);
        void SetAsColletable();
    }
}
