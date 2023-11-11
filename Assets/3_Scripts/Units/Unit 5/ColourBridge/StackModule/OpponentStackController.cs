using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class OpponentStackController
    {
        public Opponent opponent;

        private Stack<IStack> _stackQueue;
        public OpponentStackController(Opponent _opponent)
        {
            opponent = _opponent;
            _stackQueue = new Stack<IStack>();
        }
        
        public void AddStack(IStack stack, Transform parent)
        {
            if(!opponent.CanCollectStack) return;

            _stackQueue.Push(stack);
            stack.CanCollectable = false;
            stack.Collect(opponent, parent, opponent.StackCount);

            opponent.StackCount++;
            opponent.PlaySound(OpponentAudioType.COLLECT_STACK);
            opponent.OpponentController.StackAdded();
        }

        public void RemoveStack (Vector3 targetPosition)
        {
            IStack stack = _stackQueue.Pop();
            opponent.StackCount--;

            stack.SetAsStairStep = true;
            stack.SetAsStep(targetPosition);

            opponent.PlaySound(OpponentAudioType.BRING_STEP);
            opponent.OpponentController.StackRemoved();
        }

        public void DropAllStack(bool canCollectStack, bool destroyAfer = false)
        {
            opponent.CanMove = canCollectStack;
            opponent.CanCollectStack = canCollectStack;
            while (_stackQueue.Count > 0)
            {
                IStack iStack = _stackQueue.Pop();
                iStack.DropStack(destroyAfer);
                iStack.SetColor(ColorType.None, opponent.colorData);
            }
            opponent.StackCount = 0;
        }
    }
}
