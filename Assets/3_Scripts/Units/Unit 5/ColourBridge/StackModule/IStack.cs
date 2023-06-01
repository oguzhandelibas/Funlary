using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public interface IStack
    {
        void MoveTo(Transform parent, float height);
        void DropStack();
    }
}
