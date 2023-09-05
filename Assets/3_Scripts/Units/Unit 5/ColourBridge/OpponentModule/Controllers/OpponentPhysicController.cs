using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule.Animation;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class OpponentPhysicController : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public Opponent opponent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStack iStack) && iStack.CanCollectable)
            {
                opponent.stackController.AddStack(iStack ,opponent.stackParent);
            }
            
            if (opponent.HasStack && other.TryGetComponent(out IStep iStep))
            {
                StepManager stepManager = iStep.GetStepManager();

                print("Step Color: " + (iStep.StepColorType) + " , " 
                      + "Opponent Color: " + (opponent.ColorType) + " , " 
                      + "Step Used: " + (!iStep.Used));
                if (iStep.StepColorType == opponent.ColorType && !iStep.Used)
                {
                    // Step'in rengi Opponentin rengi ile aynýysa ve Step kullanýlmamýþsa
                    if (stepManager.ActivateStep(iStep.Index+1, opponent.StackCount, opponent.GetColor(), opponent.ColorType))
                    {
                        opponent.stackController.RemoveStack(iStep.Position());
                    }
                }
                else if (iStep.StepColorType != opponent.ColorType && iStep.Used)
                {
                    iStep.Used = false;
                    if (stepManager.ActivateStep(iStep.Index+1, opponent.StackCount, opponent.GetColor(), opponent.ColorType))
                    {
                        opponent.stackController.RemoveStack(iStep.Position());
                    }
                }
            }
        }
        
//Collision Detection for Opponent Crash: DropIt and AddForce
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicController opponentPhysicController))
            {
                Opponent targetOpponent = opponentPhysicController.opponent;
                rigidbody.velocity = Vector3.zero;
                if (targetOpponent.StackCount <=  opponent.StackCount)
                {
                    targetOpponent.stackController.DropAllStack();
                    targetOpponent.character.DOLookAt(opponent.character.position, 0.25f);
                    targetOpponent.opponentMovement.animationController.PlayAnim(AnimTypes.FALL);
                }
                
            }
        }
    }

    
}
