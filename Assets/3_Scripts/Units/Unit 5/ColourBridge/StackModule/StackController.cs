using System.Collections;
using System.Collections.Generic;
using Funlary.Unit5.OpponentModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class StackController
    {
        public Opponent opponent;
        private Queue<IStack> stacks;
        private float height;
        public StackController(Opponent _opponent)
        {
            opponent = _opponent;
            stacks = new Queue<IStack>();
            height = 0;
        }
        
        public void AddStack(IStack stack, Transform parent)
        {
            height++;
            opponent.StackCount++;
            stack.MoveTo(parent, height);
            stacks.Enqueue(stack);
        }
        
        public IStack RemoveStack()
        {
            return stacks.Dequeue();
        }

        public void DropAllStack()
        {
            while (stacks.Count > 0)
            {
                stacks.Dequeue().DropStack();
            }

            height = 0;
            opponent.StackCount = 0;
        }
    }
}
