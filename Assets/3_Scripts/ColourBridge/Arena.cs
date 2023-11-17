using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary
{
    public class Arena : MonoBehaviour
    {
        public GroundBounds groundBounds;
        public StackManager stackManager;
        
        [Header("Enter Door Properties")]
        public bool HasEnterDoor;
        public int enterDoorCount;

        [Header("Exit Door Properties")]
        public bool HasExitDoor;
        public int exitDoorCount;
        
        public void SetArenaBound()
        {
            groundBounds.SetArenaBound();
        }

        public void DeleteArenaBound()
        {
            groundBounds.DeleteArenaBound();
        }
    }
}
