using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Funlary.UIModule.Menu.Unit
{
    [CreateAssetMenu(fileName = "GameSelectionPanelData", menuName = "ScriptableObjects/UIModule/Units/GameSelectionPanelData", order = 1)]
    public class GameSelectionPanelData : ScriptableObject
    {
        //public List<GameObject> gameSelectionPanels;
        [SerializedDictionary("Panel Index", "Panel Object")]
        public SerializedDictionary<int, GameObject> GameSelectionPanels;
    }
}
