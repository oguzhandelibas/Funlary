using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class StackController
    {
        //Opponent stack yönetimi buradan
        private Queue<Stack> stacks;

        public void AddStack(Stack stack)
        {
            stacks.Enqueue(stack);
        }
        
        public Stack RemoveStack()
        {
            return stacks.Dequeue();
        }
    }
}
