using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public interface IControl
    {
        Vector3 MoveDirection();
        Vector3 Stop();
    }
}