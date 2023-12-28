using System;
using System.Threading.Tasks;
using Funlary.InventoryModule;
using Funlary.LevelModule.Data;
using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary
{
    public class GameManager : AbstractSingleton<GameManager>
    {
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private PlayfabManager playfabManager;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private LevelData[] levelDatas;
        private GameObject _currentGameObject;
        
        private int _levelIndex;
        public int LevelIndex
        {
            get
            {
                return PlayerPrefs.GetInt("LevelCount");
            }
            set
            {
                PlayerPrefs.SetInt("LastTime", (int)timeManager.CurrentTime);
                _levelIndex = value;
                if (_levelIndex >= levelDatas.Length)
                {
                    PlayerPrefs.SetInt("BestTime", (int)timeManager.CurrentTime);
                    PlayfabManager.Instance.SendLeaderboard((int)timeManager.CurrentTime);
                    _ResetGame();
                    PlayerPrefs.SetInt("LevelCount", _levelIndex);
                    _levelIndex = 0;
                }
                PlayerPrefs.SetInt("LevelCount", _levelIndex);
                SetLevel();
                GameUIManager.Instance.Show<GameUI>();
            }
        }

        public bool soundIsActive;

        public void _SoundActiveness(bool value)
        {
            soundIsActive = value;
        }
        private void Start()
        {
            _SoundActiveness(true);
            _levelIndex = LevelIndex;
        }

        public void PlayGame()
        {
            SetLevel();
            timeManager.ActivateTimer();
        }

        private Task SetLevel()
        {
            if(_currentGameObject) Destroy(_currentGameObject);
            Task.Delay(500);
            if (LevelIndex > 0) timeManager.CurrentTime = PlayerPrefs.GetInt("LastTime");
            _currentGameObject = Instantiate(levelDatas[LevelIndex].GamePrefab);
            return Task.CompletedTask;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("LastTime", (int)timeManager.CurrentTime);
        }

        public void _ResetGame()
        {
            _levelIndex = 0;
            inventoryManager.ResetInventory();
            timeManager.ResetTime();
            LevelIndex = 0;
            SetLevel();
        }
    }
}
