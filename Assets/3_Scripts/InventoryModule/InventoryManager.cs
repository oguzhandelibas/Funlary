using System.Collections;
using System.Collections.Generic;
using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary.InventoryModule
{
    public class InventoryManager : AbstractSingleton<InventoryManager>
    {
        [SerializeField] private InventoryData inventoryData;

        private void Start()
        {
            GameUIManager.Instance.gameUI.SetCoinText(inventoryData.Coin.ToString());
        }

        public void AddCoin(int addValue)
        {
            inventoryData.Coin += addValue;
            GameUIManager.Instance.gameUI.SetCoinText(inventoryData.Coin.ToString());
        }

        public void RemoveCoin(int removeValue)
        {
            inventoryData.Coin -= removeValue;
            GameUIManager.Instance.gameUI.SetCoinText(inventoryData.Coin.ToString());
        }

        public void ResetInventory()
        {
            inventoryData.Coin = 0;
            GameUIManager.Instance.gameUI.SetCoinText(inventoryData.Coin.ToString());
        }
    }
}
