using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.UIModule.Menu.Unit
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UIModule/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        public UnitTypes UnitTypes;
        public string UnitExplanation;
    }
}
