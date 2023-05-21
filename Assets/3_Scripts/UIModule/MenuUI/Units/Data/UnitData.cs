using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Funlary.UIModule.Menu.Unit
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UIModule/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        public UnitType UnitType;
        public string UnitExplanation;
        public List<GameObject> UnitGamePrefabs;
    }
}
