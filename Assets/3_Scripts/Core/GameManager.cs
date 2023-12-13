using System;
using System.Threading.Tasks;
using Funlary.LevelModule.Data;
using Funlary.UIModule.Core;
using Funlary.UIModule.Game;
using UnityEngine;

namespace Funlary
{
    public class GameManager : AbstractSingleton<GameManager>
    {
        [SerializeField] private PlayfabManager playfabManager;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private LevelData[] levelDatas;
        private GameObject _currentGameObject;
        
        private int _levelIndex;
        public int LevelIndex
        {
            get
            {
                if (_levelIndex >= levelDatas.Length)
                {
                    PlayerPrefs.SetInt("BestTime", (int)timeManager.CurrentTime);
                    playfabManager.SendLeaderboard(PlayerPrefs.GetInt("BestTime"));
                    timeManager.ResetTime();
                    _levelIndex = 0;
                    PlayerPrefs.SetInt("LevelCount", _levelIndex);
                }
                return PlayerPrefs.GetInt("LevelCount", 0);
            }
            set
            {
                _levelIndex = value;
                PlayerPrefs.SetInt("LevelCount", _levelIndex);
                SetLevel();
                GameUIManager.Instance.Show<GameUI>();
            }
        }
        
        private void Start()
        {
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
            _currentGameObject = Instantiate(levelDatas[LevelIndex].GamePrefab);
            return Task.CompletedTask;
        }
    }
}
