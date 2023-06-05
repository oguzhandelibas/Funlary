using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class StackController
    {
        public Opponent opponent;
        private Stack<IStack> StackQueue; // FIFO DEĞL LIFO???
        
        public StackController(Opponent _opponent)
        {
            opponent = _opponent;
            StackQueue = new Stack<IStack>();
        }
        
        public void AddStack(IStack stack, Transform parent)
        {
            opponent.StackCount++;
            stack.CanCollectable = false;
            stack.MoveTo(parent, opponent.StackCount);
            StackQueue.Push(stack);
        }

        public void RemoveStack (Vector3 targetPosition)
        {
            IStack stack = StackQueue.Pop();
            opponent.StackCount--;
            stack.SetAsStairStep = true;
            stack.MoveTo(targetPosition);
        }

        public void DropAllStack()
        {
            while (StackQueue.Count > 0)
            {
                StackQueue.Pop().DropStack();
                
            }
            opponent.StackCount = 0;
        }
    }
}
