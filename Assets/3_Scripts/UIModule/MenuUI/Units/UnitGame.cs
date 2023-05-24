using System;
using System.Collections;
using System.Collections.Generic;
using Funlary.UIModule.Menu.Screens;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Funlary.UIModule.Menu.Unit
{
    public class UnitGame : MonoBehaviour
    {
        [SerializeField] private int GameIndex;
        
        public void _CreateGame(){
            MenuUIManager.Instance.GameIndex = GameIndex;
            int unitIndex = MenuUIManager.Instance.UnitIndex;
            GameObject gamePrefab = UnitManager.Instance.UnitDatas[unitIndex].UnitGamePrefabs[GameIndex];
            BootLoader.Instance.CreateGameLevel(gamePrefab);
        }
    }
}
