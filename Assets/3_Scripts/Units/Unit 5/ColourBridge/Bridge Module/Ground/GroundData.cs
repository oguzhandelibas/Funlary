using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.ColourBridge.BridgeModule
{
    [CreateAssetMenu(fileName = "GroundData", menuName = "BridgeModule/GrounData", order = 1)]
    public class GroundData : ScriptableObject
    {
        [Header("Enter Door Properties")]
        public bool HasEnterDoor;

        [Header("Exit Door Properties")]
        public bool HasExitDoor;
        public int exitDoorCount;
    }
}
