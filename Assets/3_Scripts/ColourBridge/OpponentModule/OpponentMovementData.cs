using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "OpponentModule/MovementData", order = 1)]
    public class OpponentMovementData : ScriptableObject
    {
        public float MovementSpeed = 500;
        public bool DoubleSpeed = false;
    }
}
