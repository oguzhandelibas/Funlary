using DG.Tweening;
using Funlary.InventoryModule;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule.Animation;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary.Unit5.OpponentModule.Controller
{
    public class OpponentPhysicsController : MonoBehaviour
    {
        #region FIELDS
        public Opponent opponent;
        #endregion

        #region UNITY FUNCTIONS
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out StackManager stackManager))
            {
                if (stackManager.HasThisOpponent(opponent)) return;
                stackManager.AddOpponent(opponent);
                stackManager.StackActiveness(!stackManager.activeOnStart);

                opponent.currentStackManager = stackManager;
                opponent.DropAllStacks(true, true);
                opponent.SetColor(OpponentManager.Instance.GetRandomColorType(opponent.ColorType));
                opponent.SetBridges(stackManager.groundBounds.GetBridges());
            }

            if (other.TryGetComponent(out IStack iStack) && iStack.CanCollectable &&
                (iStack.StackColorType == ColorType.None || iStack.StackColorType == opponent.ColorType))
            {
                opponent.OpponentStackController.AddStack(iStack, opponent.stackParent);
                iStack.SetColor(opponent.ColorType, opponent.colorData);
            }

            if (opponent.HasStack && other.TryGetComponent(out IStep iStep))
            {
                StepManager stepManager = iStep.GetStepManager();
                if (iStep.StepColorType == opponent.ColorType && !iStep.Used)
                {
                    // Step'in rengi Opponentin rengi ile ayn�ysa ve Step kullan�lmam��sa
                    if (stepManager.ActivateStep(iStep.Index, opponent.StackCount, opponent.GetColor, opponent.ColorType))
                    {
                        opponent.OpponentStackController.RemoveStack(iStep.Position());
                        InventoryManager.Instance.AddCoin(1);
                    }
                }
                else if (iStep.StepColorType != opponent.ColorType && iStep.Used)
                {
                    iStep.Used = false;
                    if (stepManager.ActivateStep(iStep.Index, opponent.StackCount, opponent.GetColor, opponent.ColorType))
                    {
                        opponent.OpponentStackController.RemoveStack(iStep.Position());
                    }
                }
                
            }
        }

        //Collision Detection for Opponent Crash: DropIt and AddForce
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out OpponentPhysicsController opponentPhysicController))
            {
                Opponent targetOpponent = opponentPhysicController.opponent;
                opponent.opponentMovement.rb.velocity = Vector3.zero;
                if (targetOpponent.StackCount <= opponent.StackCount)
                {
                    targetOpponent.DropAllStacks(true, false);
                    //targetOpponent.character.DOLookAt(opponent.character.position, 0.25f);
                    //targetOpponent.opponentMovement.animationController.PlayAnim(AnimTypes.FALL);
                }
            }
        }

        #endregion
    }


}
