using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.InventoryModule
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "InventoryModule/InventoryData", order = 1)]
    public class InventoryData : ScriptableObject
    {
        public int Coin;
    }
}
