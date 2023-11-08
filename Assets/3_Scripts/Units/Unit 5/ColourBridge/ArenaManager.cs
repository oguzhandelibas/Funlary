using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Funlary.Unit5.OpponentModule;
using Funlary.Unit5.StackModule;
using UnityEngine;

namespace Funlary
{
    public class ArenaManager : MonoBehaviour
    {
        [SerializeField] private GroundBounds[] groundBoundsArray;

        private async Task Start()
        {
            return;
            foreach (GroundBounds groundBounds in groundBoundsArray)
            {
                groundBounds.SetArenaBound();
                await Task.Delay(150);
            }
        }
    }
}
