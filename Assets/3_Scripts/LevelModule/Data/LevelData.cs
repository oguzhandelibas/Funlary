using UnityEngine;

namespace Funlary.LevelModule.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelModule/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public GameObject GamePrefab;
    }
}
