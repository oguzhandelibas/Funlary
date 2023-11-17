using Funlary.LevelModule.Data;
using UnityEngine;

namespace Funlary
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelData[] levelDatas;

        public int LevelIndex
        {
            get
            {
                return PlayerPrefs.GetInt("LevelCount", 0);
            }
            set
            {
                LevelIndex = value;
                PlayerPrefs.SetInt("LevelCount", value);
            }
        }
        
        private void Start()
        {
            print("selam sisko " + LevelIndex);
            Instantiate(levelDatas[LevelIndex].GamePrefab);
        }
    }
}
