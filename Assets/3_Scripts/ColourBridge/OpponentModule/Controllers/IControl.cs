using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public interface IControl
    {
        void StackAdded();
        void StackRemoved();
        Vector3 MoveDirection();
        Vector3 Stop();
    }
}