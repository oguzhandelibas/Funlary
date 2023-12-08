using System;
using UnityEngine;

namespace Funlary
{
    public class ArenaManager : MonoBehaviour
    {
        public Arena[] arenas;

        private void Start()
        {
            for (int i = 0; i < arenas.Length; i++)
            {
                arenas[i].SetArenaBound();
                arenas[i].index = i;
                arenas[i].ArenaManager = this;
            }
        }

        public Arena GetNextArena(int currentArenaIndex)
        {
            return arenas[currentArenaIndex + 1];
        }
    }
}
