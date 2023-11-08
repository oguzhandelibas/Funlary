using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class PlayerController : IControl
    {
        public JoystickController joystickController;

        public void StackAdded() { }
        public void StackRemoved() { }

        public Vector3 MoveDirection()
        {
            Vector3 direction = joystickController.JoystickInput();
            return direction;
        }

        public Vector3 Stop()
        {
            return Vector3.zero;
        }
    }
}
