using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class OpponentPhysicController : MonoBehaviour
    {
        [SerializeField] private Opponent opponent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStack iStack) && iStack.CanCollectable())
            {
                print("Topla");
                opponent.stackController.AddStack(iStack ,opponent.stackParent);
            }
        }
        
//Collision Detection for Opponent Crash: DropIt and AddForce
        private void OnCollisionEnter(Collision other)
        {
            print("Bum!");
            opponent.stackController.DropAllStack();
        }
    }

    
}
