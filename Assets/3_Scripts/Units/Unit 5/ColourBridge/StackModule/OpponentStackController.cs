using System.Collections;
using System.Collections.Generic;
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
            opponent.StackCount++;
            stack.CanCollectable = false;
            stack.MoveTo(parent, opponent.StackCount);
            _stackQueue.Push(stack);
        }

        public void RemoveStack (Vector3 targetPosition)
        {
            IStack stack = _stackQueue.Pop();
            opponent.StackCount--;

            stack.SetAsStairStep = true;
            stack.SetAsStep(targetPosition);
        }

        public void DropAllStack(bool canCollectStack = true)
        {
            opponent.CanMove = canCollectStack;
            opponent.CanCollectStack = canCollectStack;
            while (_stackQueue.Count > 0)
            {
                _stackQueue.Pop().DropStack();
            }
            opponent.StackCount = 0;
        }
    }
}
